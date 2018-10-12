using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interface
{
    public interface ICompression
    {
        byte[] Compress(byte[] plaintext);
        byte[] Decompress(byte[] compress);
    }
}
