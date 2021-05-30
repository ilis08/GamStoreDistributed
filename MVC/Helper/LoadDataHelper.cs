using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Helper
{
    public class LoadDataHelper
    {
        public static SelectList LoadCategoryData()
        {
            using (ServiceReference1.Service1Client service = new ServiceReference1.Service1Client())
            {
                return new SelectList(service.GetCategories(), "Id", "Title");
            }
        }
	}
}