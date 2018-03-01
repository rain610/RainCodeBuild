using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo
{
    public class StatisticsService
    {
        private readonly IUserService _userService;
        public StatisticsService(IUserService userService)
        {
            _userService = userService;
        }

        public bool Test()
        {
            return _userService.Test();
        }
    }
}
