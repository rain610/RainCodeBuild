using Common.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;

namespace Common
{
    [MapTo(typeof(ICompression))]
    public class GZipCompression: ICompression
    {
        private static Semaphore semaphore = new Semaphore(4, 4);
        public byte[] Compress(byte[] plaintext)
        {
            var result = new byte[0];
            if (plaintext != null && plaintext.Any() && semaphore.WaitOne(TimeSpan.FromSeconds(7)))
            {
                try
                {
                    using (var input = new MemoryStream(plaintext))
                    {
                        using (var output = new MemoryStream())
                        {
                            using (var compress = new GZipStream(output, CompressionMode.Compress))
                            {
                                input.CopyTo(compress);
                            }
                            result = output.ToArray();
                        }
                    }
                    semaphore.Release();
                }
                catch (Exception ex)
                {
                    semaphore.Release();
                    throw;
                }
            }
            return result;
        }

        public byte[] Decompress(byte[] compress)
        {
            var result = new byte[0];
            if ((compress != null) && (compress.Any()) && semaphore.WaitOne(TimeSpan.FromSeconds(7)))
            {
                try
                {
                    using (var input = new MemoryStream(compress))
                    {
                        using (var output = new MemoryStream())
                        {
                            using (var decompress = new GZipStream(input, CompressionMode.Decompress))
                            {
                                decompress.CopyTo(output);
                            }
                            result = output.ToArray();
                        }
                    }
                    semaphore.Release();
                }
                catch (Exception exception)
                {
                    semaphore.Release();
                    throw;
                }
            }
            return result;
        }
    }
}
