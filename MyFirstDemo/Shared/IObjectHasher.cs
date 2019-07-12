using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IObjectHasher
    {
        byte[] ComputeHash(object value);
        byte[] ComputeHash(byte[] value);

        string ComputHashAsBase64String(object value);
    }
}
