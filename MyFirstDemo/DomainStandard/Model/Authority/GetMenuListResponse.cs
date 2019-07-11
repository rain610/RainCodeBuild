using System;
using System.Collections.Generic;
using System.Text;

namespace DomainStandard.Model.Authority
{
    public class GetMenuListResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public IEnumerable<GetMenuListModel> Menus { get; set; }
    }
}
