using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public interface IHashPayloadProvider
    {
        byte[] GetPayload(object value);
    }
}
