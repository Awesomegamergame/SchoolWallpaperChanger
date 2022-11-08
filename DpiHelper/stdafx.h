#pragma once

#include <Windows.h>
#include <vector>
#include <cassert>
#include <fstream>

// reference additional headers your program requires here
#ifdef __cplusplus
extern "C" {
#endif
    extern __declspec(dllexport) void PrintDpiInfo(char* str);
    extern __declspec(dllexport) void SetDPIScaling(INT32 adapterIDHigh, UINT32 adapterIDlow, UINT32 sourceID, UINT32 dpiPercentToSet);
    extern __declspec(dllexport) void RestoreDPIScaling();
#ifdef __cplusplus
}
#endif
