using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZeiBook.DoTask.Data;
using System.Linq;
using System.Diagnostics;
using ZeiBook.Models;
using ZeiBook.DoTask.Models;
using ZeiBook.Data;
using ZeiBook.DoTask.Actions;

namespace ZeiBook.Test
{
    [TestClass]
    public class DataTranTest
    {
        [TestMethod]
        public void LookClass()
        {
            BookDbContext context = BookContextBuilder.GetAppContext();
            var list = context.BookDatas.Select(t => t.Class).Distinct();
            foreach (var item in list)
            {
                Debug.WriteLine(item);
            }
        }

        [TestMethod]
        public void LookCategory()
        {
            ApplicationDbContext context = ContextBuilder.GetAppContext();
            var list = context.Categories;
            foreach (var item in list)
            {
                Debug.Write(item.Name + " ");
            }
        }

        [TestMethod]
        public void DoTran()
        {
            //ApplicationDbContext context = ContextBuilder.GetAppContext();
            //BookDbContext bookContext = BookContextBuilder.GetAppContext();
            //TranAction action = new TranAction(context);
            //var list = context.Categories;
            //int count = 0;
            //foreach (var item in bookContext.BookDatas)
            //{
            //    if (count++ % 100 == 1)
            //    {
            //        context.SaveChanges();
            //    }
            //    var bookItem = action.GetBook(item);
            //    if (!context.Books.Any(t => t.Name == bookItem.Name && t.Author == bookItem.Author))
            //    {
            //        context.Books.Add(bookItem);
            //    }
            //}
            //context.SaveChanges();
        }


    }
}
