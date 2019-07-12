using DomainStandard.Interface;
using DomainStandard.Model;
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
        private readonly IObjectHasher _hasher;
        public StudentAService(IObjectHasher hasher)
        {
            _hasher = hasher;
        }
        public async Task SyncDataAsync()
        {
            var test = new EmployeeModel();
            test.Hash = _hasher.ComputeHash(test);
            //SequenceEqual  比较数组
        }

        public async Task SyncOneAsync()
        {

        }
    }
}
