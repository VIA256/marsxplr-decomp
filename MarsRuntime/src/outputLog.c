#include "outputLog.h"

#include <stdlib.h>
#include <assert.h>
#include <stdio.h>
#include <stdbool.h>

struct OutputLog *MX_outputLogCreate(
	const char *fname,
	enum OutputLogLevel tolerance)
{
	struct OutputLog *ol = calloc(1, sizeof(struct OutputLog));
	assert(ol);
	
#ifdef _WIN32
	FILE *fd;
	if(fopen_s(&fd, fname, "w") != 0)
	{
		free(ol);
		return NULL;
	}
#else
	FILE *fd = fopen(fname, "w");
	if(fd == NULL)
	{
		free(ol);
		return NULL;
	}
#endif
	
	ol->stream = fd;
	ol->tolerance = tolerance;
	
	return ol;
}
	
bool MX_outputLogDestroy(struct OutputLog *ol)
{
	bool success = (fclose(ol->stream) == 0);
	free(ol);
	return success;
}

void MX_outputLogWriteLine(
	struct OutputLog *ol,
	enum OutputLogLevel level,
	const char *str)
{
	if(ol->tolerance > level) return;
	
	const char *logHdr;
	switch(level)
	{
	case MX_OUTPUT_LOG_INFO:
		logHdr = "II";
		break;
	case MX_OUTPUT_LOG_WARN:
		logHdr = "WW";
		break;
	case MX_OUTPUT_LOG_ERROR:
		logHdr = "EE";
		break;
	default:
		logHdr = "??";
		break;
	}
	
	fprintf(ol->stream, "%s\t%s\n", logHdr, str);
#ifndef NDEBUG
	printf("%s\t%s\n", logHdr, str);
#endif
}