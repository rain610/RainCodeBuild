using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Context;

namespace WebDemo.Pages
{
    public class FirstBloodModel : PageModel
    {
        private readonly IOptions<MessageQueueOptions> _options;
        public FirstBloodModel(IOptions<MessageQueueOptions> options)
        {
            _options = options;
        }
        public void OnGet()
        {
            var result = _options.Value.ConsumerName;
        }
    }
}