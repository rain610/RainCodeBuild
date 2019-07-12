using DomainStandard.Interface;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainStandard.Services
{
    [MapTo(typeof(IStudentAService))]
    public class StudentAService: IStudentAService
    {
        public async Task SyncDataAsync()
        {

        }

        public async Task SyncOneAsync()
        {

        }
    }
}
