using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeiBook.Data;
using ZeiBook.Models.PageViewModels;
using ZeiBook.Actions.Search;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZeiBook.Controllers
{
    public class SearchController : Controller
    {

        public SearchController()
        {
        }

        [HttpGet("/Search",Order =1)]
        [HttpGet("/Search/p{pageNum:int}", Order =0)]
        public IActionResult Index(string keyword,int? pageNum,[FromServices]SearchAction action)
        {
            var model = action.GetViewModel(keyword,pageNum??1);
            if (model==null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
