using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models.Utils
{
    public class SearchPageOption:PageOption
    {
        public RouteValueDictionary Routes { get; set; }
        public String ActionName { get; set; }
        public String ControllerName { get; set; }
    }
}
