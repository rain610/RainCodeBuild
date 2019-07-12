using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HashMemberAttribute : Attribute
    {
    }
}
