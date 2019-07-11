using System;
using System.Collections.Generic;
using System.Text;

namespace DomainStandard.Model.Authority
{
    public class GetMenuListModel
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int OrderNo { get; set; }
        public int Type { get; set; }
        public string Remark { get; set; }
        public IEnumerable<GetMenuListModel> ChildMenus { get; set; }
    }
}
