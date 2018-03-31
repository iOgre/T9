using System;

namespace T9Parser
{
    [Flags]
    public enum ConversionSettings
    {
        Default = 1,
        LowercaseAll = 2,
        FailOnUnconvertable = 4
    }
}