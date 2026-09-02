/*
== file path manipulation helpers ==
NOTE: all functions that operate on path strings assume
	  that the path string is a valid path.
	  they are not to be fed string created by the user.
	  all functions that return strings are guaranteed to be 
	  newly allocated strings that must be freed later.
WARNING: if an inputted string is longer than the os's set max path length,
		 a function will not fail, but will truncate the string before
		 operating on it
*/

#ifndef MX_FS_UTIL_H
#define MX_FS_UTIL_H

#include <stdbool.h>
#include <stdarg.h>
#include <string.h>

//connects two strings into a path accounting for '/' vs '\'
char *MX_pathConcat2(const char *a, const char *b);
//concats arbitrary number of path strings
#define MX_pathConcat(...) MX_pathConcatV(0, __VA_ARGS__, NULL);
char *MX_pathConcatV(int none, ...); //dont call this directly, initial arg needed to work with MSVC

//gets rid of tailing slash
char *MX_pathParent(const char *p);

//checks if path can be stat-ed by os
bool MX_pathExists(const char *p);

#endif
