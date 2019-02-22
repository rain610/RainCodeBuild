using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface ICompression
    {
        byte[] Compress(byte[] plaintext);
        byte[] Decompress(byte[] compress);
    }
}
