using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZeiBook.Actions.Components;
using ZeiBook.Data;

namespace ZeiBook.Components
{
    public class CategoryViewComponent:ViewComponent
    {
        private CategoryAction _action;

        public CategoryViewComponent(CategoryAction action)
        {
            _action = action;
        }

        public IViewComponentResult Invoke()
        {
            return View(_action.GetList());
        }
    }
}
