#ifndef MX_MONO_RUNTIME_LINKER_H
#define MX_MONO_RUNTIME_LINKER_H

#include <mono/jit/jit.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>

#include <stdbool.h>

#ifdef _WIN32
	#include <Windows.h>
	typedef HMODULE MX_MODULE;
	typedef FARPROC MX_PTR;
#else
	typedef void * MX_MODULE;
	typedef void *(*MX_PTR)();
#endif

/// leading "mono_" stripped from names so calling mono functions is like this for example:
///     mono->config_parse(NULL);
struct MonoRuntime
{
  MX_MODULE handle;

  // void mono_set_assemblies_path(const char *assembly_dir)
  //void (*set_assemblies_path)(const char *);
  MX_PTR set_assemblies_path;

  /// void mono_set_dirs(const char *assembly_dir, const char *config_dir)
  //void (*set_dirs)(const char *, const char *);
  MX_PTR set_dirs;
  
  /// void mono_config_parse (const char *filename)
  //void (*config_parse)(const char *);
  MX_PTR config_parse;
  
  /// MonoDomain *mono_jit_init_version(const char *root_domain_name, const char *runtime_version)
  //MonoDomain *(*jit_init_version)(const char *, const char *);
  MX_PTR jit_init_version;
  
  /// void mono_add_internal_call(const char *name, const void* method);
  //void (*add_internal_call)(const char *, const void *);
  MX_PTR add_internal_call;
  
  /// MonoAssembly *mono_domain_assembly_open(MonoDomain *domain, const char *name)
  //MonoAssembly *(*domain_assembly_open)(MonoDomain *, const char *);
  MX_PTR domain_assembly_open;
  
  /// void mono_jit_cleanup(MonoDomain *domain)
  //void (*jit_cleanup)(MonoDomain *);
  MX_PTR jit_cleanup;
  
  /// void mono_jit_exec(MonoDomain *domain, MonoAssembly *assembly, int argc, char *argv[])
  //void (*jit_exec)(MonoDomain *, MonoAssembly *, int, char *[]);
  MX_PTR jit_exec;
};

struct MonoRuntime *MX_monoRuntimeCreate(char *monoLibPath);
bool MX_monoRuntimeDestroy(struct MonoRuntime *);

#endif
