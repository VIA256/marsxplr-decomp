#include <mono/jit/jit.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>

#include <stdio.h>
#include <stdbool.h>

#include "FSUtil.h"
#include "monoRuntimeLinker.h"
#include "outputLog.h"

#ifdef _WIN32
	#pragma comment(lib, "user32.lib")
	#include <Windows.h>
#endif
void errorMessage(const char *msg)
{
#ifdef _WIN32

	MessageBoxA(
		NULL,
		msg,
		"Mars Explorer Error",
		MB_ICONERROR | MB_TASKMODAL);

#else

	fprintf(stderr, "%s\n", msg);

#endif
}

struct OutputLog *OL;

void RegisterDomainAndLoadAssemblies()
{
	MX_LOG_WARN(OL, "this doesn't do anything yet");
}




struct MonoRuntime *initMono(const char *execPath)
{
#ifdef _WIN32
	const char *monoRtlName = "mono-runtime.dll";
#elif defined __APPLE__
	const char *monoRtlName = "mono-runtime.dylib";
#else
	const char *monoRtlName = "mono-runtime.so";
#endif

	char *monoRtlPath = MX_pathConcat(
		execPath,
		"Mars Explorer_Data",
		"unity",
		monoRtlName);
	if(!MX_pathExists(monoRtlPath))
	{
		errorMessage("failed to locate mono runtime shared library");
		printf("libpath: %s\n", monoRtlPath);
		free(monoRtlPath);
		
		return NULL;
	}
	
	struct MonoRuntime *mono = MX_monoRuntimeCreate(monoRtlPath);
	free(monoRtlPath);
	if(mono == NULL)
	{
		errorMessage("failed to link to mono runtime");

		return NULL;
	}

	char *libPath = MX_pathConcat(
		execPath,
		"Mars Explorer_Data",
		"lib");
	if(!MX_pathExists(libPath))
	{
		errorMessage("could not find lib path in Data directory");
		free(libPath);
		MX_monoRuntimeDestroy(mono);

		return NULL;
	}
	
	char *etcPath = MX_pathConcat(
		execPath,
		"Mars Explorer_Data",
		"etc");
	if(!MX_pathExists(etcPath))
	{
		errorMessage("could not find etc path in Data directory");
		free(etcPath);
		MX_monoRuntimeDestroy(mono);

		free(libPath);
		return NULL;
	}
	
	mono->set_dirs(libPath, etcPath);
	free(etcPath);
	mono->set_assemblies_path(libPath);
	free(libPath);
	mono->config_parse(NULL);

	return mono;
}





int main(int argc, char **argv)
{
	if(argc < 1)
	{
		errorMessage("unable to determine executable path\n");
		return -1;
	}
	
	char *execPath = MX_pathParent(argv[0]);
	if(!MX_pathExists(execPath))
	{
		errorMessage("executable path determined from argv[0] does not exist as a valid path");
		free(execPath);
		return -1;
	}

	struct MonoRuntime *mono = initMono(execPath);
	if(mono == NULL)
	{
		free(execPath);
		return -1;
	}
	
	char *outputLogPath = MX_pathConcat(execPath, "Mars Explorer_Data", "output_log.txt");
	OL = MX_outputLogCreate(outputLogPath, MX_OUTPUT_LOG_INFO);
	if(OL == NULL)
	{
		errorMessage("failed to setup output_log.txt for writing");
		free(execPath);
		return -1;
	}
	free(outputLogPath);

	char *uniDomLoaPath = MX_pathConcat(execPath, "Mars Explorer_Data", "lib", "UnityDomainLoad.exe");
	MonoDomain *domain = (MonoDomain *)mono->jit_init_version(uniDomLoaPath, "v4.0");
	if(domain == NULL)
	{
		MX_LOG_ERROR(OL, "failed to init mono jit on UnityDomainLoad.exe");
		free(uniDomLoaPath);

		free(execPath);
		MX_monoRuntimeDestroy(mono);
		return -1;
	}

	mono->add_internal_call(
		"UnityEngine.UnityDomainLoad::RegisterDomainAndLoadAssemblies",
		(const void *)RegisterDomainAndLoadAssemblies);

	MonoAssembly *assembly = (MonoAssembly *)mono->domain_assembly_open(domain, uniDomLoaPath);
	free(uniDomLoaPath);
	if(assembly == NULL)
	{
		MX_LOG_ERROR(OL, "failed to open UnityDomainLoad.exe assembly");
		
		free(execPath);
		mono->jit_cleanup(domain);
		MX_monoRuntimeDestroy(mono);
		return -1;
	}

	mono->jit_exec(domain, assembly, argc, argv);

	free(execPath);
	mono->jit_cleanup(domain);
	bool safeCleanup = true;
	if(!MX_outputLogDestroy(OL))
	{
		errorMessage("failed to cleanly close output_log.txt");
		safeCleanup = false;
	}
	if(!MX_monoRuntimeDestroy(mono))
	{
		errorMessage("failed to cleanly close mono runtime");
		safeCleanup = false;
	}
	return safeCleanup ? 0 : -1;
}
