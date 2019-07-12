using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public abstract class ObjectHasherBase : IObjectHasher
    {
        public IHashPayloadProvider PayloadProvider { get; }

        public ObjectHasherBase(IHashPayloadProvider provider)
        {

            this.PayloadProvider = provider;
        }

        public byte[] ComputeHash(object value)
        {
            return this.ComputeHash(this.PayloadProvider.GetPayload(value));
        }

        public string ComputHashAsBase64String(object value)
        {
            return this.ComputHashAsBase64String(this.PayloadProvider.GetPayload(value));
        }

        public abstract byte[] ComputeHash(byte[] payload);

        public abstract string ComputHashAsBase64String(byte[] payload);
    }
}
