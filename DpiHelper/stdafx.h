#pragma once

//#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files
#include <windows.h>

// reference additional headers your program requires here
#ifdef __cplusplus
extern "C" {
#endif
    extern __declspec(dllexport) void PrintDpiInfo();
    extern __declspec(dllexport) void SetDPIScaling(INT32 adapterIDHigh, UINT32 adapterIDlow, UINT32 sourceID, UINT32 dpiPercentToSet);
    extern __declspec(dllexport) void RestoreDPIScaling();
#ifdef __cplusplus
}
#endif
