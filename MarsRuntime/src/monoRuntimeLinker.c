#include "monoRuntimeLinker.h"

#include <stdlib.h>
#include <assert.h>
#include <stdbool.h>

#ifdef _WIN32
#include <Windows.h>
	#define RTL_OPEN(path) LoadLibraryA(path)
	typedef DWORD RTLERROR;
	#define RTL_CLEAR_ERROR() SetLastError(0)
	#define RTL_GET_ADDRESS(handle, name) GetProcAddress(handle, name)
	#define RTL_GET_ERROR() GetLastError()
	#define RTL_CLOSE(handle) FreeLibrary(handle)
#else
#include <dlfcn.h>
	#define RTL_OPEN(path) dlopen(path, RTLD_LAZY)
	typedef char * RTLERROR;
	#define RTL_CLEAR_ERROR() dlerror()
	#define RTL_GET_ADDRESS(handle, name) dlsym(handle, name)
	#define RTL_GET_ERROR() dlerror()
	//#define RTL_CLOSE(handle) (dlclose(handle) == 0)
	bool RTL_CLOSE(void *handle)
	{
    return (dlclose(handle) == 0);
	}
#endif

struct MonoRuntime *MX_monoRuntimeCreate(char *monoLibPath)
{
	void *monoHnd = RTL_OPEN(monoLibPath);
  if(monoHnd == NULL) return NULL;

  struct MonoRuntime *mono = calloc(1, sizeof(struct MonoRuntime));
  assert(mono);

  RTLERROR error;

  RTL_CLEAR_ERROR();
  mono->set_assemblies_path = RTL_GET_ADDRESS(monoHnd, "mono_set_assemblies_path");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->set_dirs = RTL_GET_ADDRESS(monoHnd, "mono_set_dirs");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->config_parse = RTL_GET_ADDRESS(monoHnd, "mono_config_parse");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->jit_init_version = RTL_GET_ADDRESS(monoHnd, "mono_jit_init_version");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->add_internal_call = RTL_GET_ADDRESS(monoHnd, "mono_add_internal_call");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->domain_assembly_open = RTL_GET_ADDRESS(monoHnd, "mono_domain_assembly_open");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->jit_cleanup = RTL_GET_ADDRESS(monoHnd, "mono_jit_cleanup");
  if((error = RTL_GET_ERROR())) goto failure;
  mono->jit_exec = RTL_GET_ADDRESS(monoHnd, "mono_jit_exec");
  if((error = RTL_GET_ERROR())) goto failure;

  mono->handle = monoHnd;
  return mono;

failure:
  RTL_CLOSE(monoHnd);
  free(mono);
  return NULL;
}

bool MX_monoRuntimeDestroy(struct MonoRuntime *mono)
{
	if(mono == NULL || mono->handle == NULL) return false;
  bool destroySuccess = RTL_CLOSE(mono->handle);
  free(mono);
  return destroySuccess;
}
