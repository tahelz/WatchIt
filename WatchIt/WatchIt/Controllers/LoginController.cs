using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

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
            SqlConnection sqlcon = new SqlConnection (@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True");

            // Models.Customer user1 = db.Customers.Where(p => p.Email == email && x.Password == password).First();
            IQueryable<Customer> users;
            users = db.Customers.Where(p => p.Password == "12");
            
            string q = "SELECT * FROM CUSTOMER WHERE PASSWORD = 12 ";
            SqlDataAdapter n = new SqlDataAdapter(q, sqlcon);
            DataTable d = new DataTable();
            n.Fill(d);
            Models.Customer user = new Models.Customer();
            if (user != null)
            {
                SignIn(email, user.IsAdmin);
                Session["User"] = user;
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
            Session["User"] = null;
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