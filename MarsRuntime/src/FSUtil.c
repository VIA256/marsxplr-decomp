#include "FSUtil.h"

#include <string.h>
#include <stdio.h>
#include <sys/stat.h> /*stat()*/
#include <stdbool.h>
#include <stdlib.h> /*(m/c)alloc free*/
#include <assert.h> /*^^^^*/
#include <stdarg.h> /*MX_pathConcat*/

#ifdef _WIN32
	#define PATH_SLASH '\\'
	
	#define MAX_PATH 260
#else
	#define PATH_SLASH '/'

	#ifdef __APPLE__
		#include <sys/syslimits.h>
	#else
		#include <linux/limits.h>
	#endif
	#ifndef PATH_MAX
		#error "PATH_MAX NOT DEFINED"
	#endif
	#define MAX_PATH PATH_MAX
#endif

bool pathIsRoot(const char *p)
{
#ifdef _WIN32
	return (strnlen(p, MAX_PATH) == 2 && p[1] == ':');
#else
	return (strncmp(p, "/", MAX_PATH) == 0);
#endif
}

char *pathTrimmed(const char *p)
{
	if(p == NULL)
	{
		char *emptyp = calloc(1, sizeof(char));
		assert(calloc);
		return emptyp;
	}

	size_t plen = strnlen(p, MAX_PATH);

	size_t truncate = pathIsRoot(p) ?
		0 :
		p[plen - 1] == PATH_SLASH ?
			1 :
			0;
			
	char *newp = calloc(plen + 1, sizeof(char));
	assert(newp);
	memcpy(newp, p, plen - truncate);
	return newp;
}

bool MX_pathExists(const char *p)
{
	struct stat sb;
	return (stat(p, &sb) == 0);
}

char *MX_pathParent(const char *p)
{
	char *tp = pathTrimmed(p);
	if(pathIsRoot(tp)) return tp;
	
	int i = ((int)strlen(tp)) - 1;
	for(; i >= 0; i--)
		if(tp[i] == PATH_SLASH) break;
	
	char *newp;
	if(i < 0)
	{
		newp = calloc(2, sizeof(char));
		assert(newp);
		newp[0] = '.';
	}
	else
	{
		newp = calloc(i + 1, sizeof(char));
		assert(newp);
		memcpy(newp, tp, i);
	}
	
	free(tp);
	return newp;
}

#ifndef strndup
char *strndup(const char *s, size_t n) {
  size_t len = strnlen(s, n);
  char *p = malloc(len + 1);
	assert(p);
  memcpy(p, s, len);
  p[len] = '\0';
  return p;
}
#endif

char *MX_pathConcat2(const char *a, const char *b)
{
	if(a == NULL && b == NULL)
	{
		char *emptyp = calloc(1, sizeof(char));
		assert(emptyp);
		return emptyp;
	}
	if(a == NULL)
	{
		char *bdup = strndup(b, MAX_PATH);
		assert(strncmp(b, bdup, MAX_PATH + 1) == 0);
		return bdup;
	}
	if(b == NULL)
	{
		char *adup = strndup(a, MAX_PATH);
		assert(strncmp(a, adup, MAX_PATH + 1) == 0);
		return adup;
	}

	char *ta = pathTrimmed(a);
	char *tb = pathTrimmed(b);

	size_t newlen = strlen(ta) + 1 + strlen(tb) + 1;
	char *newp = malloc(newlen * sizeof(char));
	assert(newp);

	size_t i = 0;
	for(size_t ia = 0; ta[ia]; ia++) newp[i++] = ta[ia];
	newp[i++] = PATH_SLASH;
	for(size_t ib = 0; tb[ib]; ib++) newp[i++] = tb[ib];
	newp[i] = '\0';

	free(ta);
	free(tb);
	return newp;
}


char *MX_pathConcatV(int none, ...)
{
	char *newp = NULL;
	char *tp;

	va_list args;
	char *val;
	va_start(args, none);
	while((val = va_arg(args, char *))){
		tp = newp;
		newp = MX_pathConcat2(tp, val);
		if(tp) free(tp);
	}
	va_end(args);

	return newp;
}
