#ifndef MX_OUTPUT_LOG_H
#define MX_OUTPUT_LOG_H

#include <stdio.h>
#include <stdbool.h>

enum OutputLogLevel
{
	MX_OUTPUT_LOG_INFO = 0,
	MX_OUTPUT_LOG_WARN = 1,
	MX_OUTPUT_LOG_ERROR = 2,
};

struct OutputLog
{
	FILE *stream;
	enum OutputLogLevel tolerance;
};

struct OutputLog *MX_outputLogCreate(
	const char *fname,
	enum OutputLogLevel tolerance);
bool MX_outputLogDestroy(struct OutputLog *);

#define MX_LOG_INFO(ol, str) MX_outputLogWriteLine(ol, MX_OUTPUT_LOG_INFO, str)
#define MX_LOG_WARN(ol, str) MX_outputLogWriteLine(ol, MX_OUTPUT_LOG_WARN, str)
#define MX_LOG_ERROR(ol, str) MX_outputLogWriteLine(ol, MX_OUTPUT_LOG_ERROR, str)
void MX_outputLogWriteLine(
	struct OutputLog *ol,
	enum OutputLogLevel level,
	const char *str);

#endif