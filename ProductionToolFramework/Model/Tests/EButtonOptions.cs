﻿using System;
namespace Demcon.ProductionTool.Model
{
    [Flags]
    public enum EButtonOptions
    {
        None = 0,
        OK = 1,
        Cancel = 2,
        Yes = 4,
        No = 8,
        GRID = 16,
        GRID4 = 32,
        Analyze = 64

    }
}