using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class GamesListViewModel
    {
        public IEnumerable<GameVM> Games { get; set; }
        public PagingModel PagingModel { get; set; }
    }
}