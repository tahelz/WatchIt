using WatchIt.DAL;
using WatchIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace WatchIt.Controllers
{
    public class CustomerController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Customer
        public ActionResult Customer()
        {
            return View();
        }

    }
}