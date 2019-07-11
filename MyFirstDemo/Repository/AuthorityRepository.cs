using DomainStandard.Interface;
using DomainStandard.Model.Authority;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    [MapTo(typeof(IAuthorityRepository), ServiceLifetime.Singleton)]
    public class AuthorityRepository: IAuthorityRepository
    {
        public async Task<GetMenuListResponse> GetMenuList(GetMenuListRequest request)
        {
            return new GetMenuListResponse();
        }
    }
}
