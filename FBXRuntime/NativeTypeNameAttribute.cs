using System;
using System.Collections.Generic;
using System.Text;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    internal sealed class NativeTypeNameAttribute : Attribute
    {
        public NativeTypeNameAttribute(string name)
        {
        }
    }
}