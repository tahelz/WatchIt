using WatchIt.DAL;
using WatchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatchIt.Controllers
{
    public class CustomerController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}