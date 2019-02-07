using System;

namespace ApiAuth
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        Customer = 1 << 0,
        Admin = 1 << 1,
        Any = ~0
    }
    //https://social.msdn.microsoft.com/Forums/vstudio/en-US/07d93c1b-c5d3-4750-abcc-6c2e21878468/enums-flags-and-extensions?forum=csharpgeneral
}