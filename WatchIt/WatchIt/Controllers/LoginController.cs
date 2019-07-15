using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using WatchIt.DAL;


namespace WatchIt.Controllers
{
    public class LoginController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // private IAuthenticationManager AuthenticationManager
        // {
           //  get
            // {
               //  return HttpContext.GetOwinContext().Authentication;
            // }
        // }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            // var user = db.Customers.Where(x => x.Email == email && x.Password == password).FirstOrDefault( );
            var user = db.Customers.Find(email);
            if (user != null)
            {
                SignIn(email, user.IsAdmin);
                Session["UserID"] = user.CustomerID;
                return Json(new { Success = true });
            }
            else
            {
                return new HttpStatusCodeResult(410, "Unable to find user.");
            }

        }

        [HttpGet]
        public ActionResult Logout()
        {
            // AuthenticationManager.SignOut();
            Session["UserID"] = null;
            Session["UserOrder"] = null;
            return RedirectToAction("Index", "Home");
        }


        private void SignIn(string userName, bool isAdmin)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));

            claims.Add(new Claim(ClaimTypes.Role, isAdmin ? "admin" : "user"));
            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            // AuthenticationManager.SignIn(id);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}