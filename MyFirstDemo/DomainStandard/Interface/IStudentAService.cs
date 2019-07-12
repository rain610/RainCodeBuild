using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainStandard.Interface
{
    public interface IStudentAService
    {
        Task SyncDataAsync();

        Task SyncOneAsync();
    }
}
