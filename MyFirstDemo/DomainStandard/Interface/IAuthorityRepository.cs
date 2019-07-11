using DomainStandard.Model.Authority;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainStandard.Interface
{
    public interface IAuthorityRepository
    {
        Task<GetMenuListResponse> GetMenuList(GetMenuListRequest request);
    }
}
