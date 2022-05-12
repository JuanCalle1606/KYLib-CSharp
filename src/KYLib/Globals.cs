//#define KYTYPES
global using bit = KYLib.MathFn.Bit;
#if KYTYPES
global using kint = KYLib.MathFn.Int;
global using kbyte = KYLib.MathFn.Small;
global using kfloat = KYLib.MathFn.Float;
global using kdouble = KYLib.MathFn.Real;
#else
global using kint = System.Int32;
global using kbyte = System.Byte;
global using kfloat = System.Single;
global using kdouble = System.Double;
#endif

global using System;

using KYLib.Internal;
using KYLib.Modding;

[assembly: ModInfo<KYLibInfo>]
[assembly: AutoLoad]
[module: AutoLoad]