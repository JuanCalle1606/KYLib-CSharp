﻿//#define KYTYPES
global using bit = KYLib.MathFn.Bit;
global using kint = System.Int32;
global using kbyte = System.Byte;
global using kfloat = System.Single;
global using kdouble = System.Double;

global using System;

using KYLib.Internal;
using KYLib.Modding;
using System.Runtime.CompilerServices;

[assembly: ModInfo<KYLibInfo>]
[assembly: AutoLoad]
[module: AutoLoad]
[assembly: InternalsVisibleTo("KYLib.Data")]
[assembly: InternalsVisibleTo("KYLib.Host")]