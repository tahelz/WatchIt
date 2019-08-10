using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WatchIt.DAL;
using WatchIt.Models;

namespace WatchIt.Controllers
{
    public class OrdersController : Controller
    {
        private WatchItContext db = new WatchItContext();

        // GET: Orders
        public ActionResult CustomerOrder()
        {
            Customer CurrentCustomer = (WatchIt.Models.Customer)System.Web.HttpContext.Current.Session["Customer"];
            var orders = db.Orders.Where(o => o.CustomerId == CurrentCustomer.CustomerID).ToList();
            
            return View(orders);
        }

        public ActionResult AllOrders()
        {   
            var AllOrders = db.Orders.ToList();

            ViewBag.branches = new SelectList(db.Branches, "BranchID", "DisplayName");
            ViewBag.customers = new SelectList(db.Customers.Where(x => x.IsAdmin == false), "CustomerID", "DisplayName");

            return View(AllOrders);
        }

        [HttpPost]
        public ActionResult AllOrders(int? branchId, int? customerId)
        {
            ViewBag.branches = new SelectList(db.Branches, "BranchID", "DisplayName");
            ViewBag.customers = new SelectList(db.Customers.Where(x => x.IsAdmin == false), "CustomerID", "DisplayName");

            var orders = db.Orders.ToList();

            if (branchId != null)
            {
                orders = orders.Where(o => o.BranchID == branchId).ToList();
            }
            if (customerId != null)
            {
                orders = orders.Where(o => o.CustomerId == customerId).ToList();
            }

            
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.Movies = db.Movies.ToList();

            return View(order);
            
        }
        
        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            order.Movies = null;
            order.TotalPrice = 0;            
            db.SaveChanges();
            return RedirectToAction("CustomerOrder");
        }


        // POST
        [HttpPost]
        public JsonResult RemoveFromOrder(int orderId, int movieId)
        {
            Order order = db.Orders.Find(orderId);
            if (order == null)
            {
                return Json(false);
            }
            if (order.Movies.Count <= 1)
            {
                return Json(false);
            }
            Movie movie = order.Movies.FirstOrDefault(p => p.ID == movieId);

            if (movie == null)
            {
                return Json(false);
            }
            order.Movies.Remove(movie);
            db.SaveChanges();
            return Json(true);
        }

        [HttpPost]
        public JsonResult AddProductToOrder(int orderId, int movieId)
        {
            Order order = db.Orders.Find(orderId);
            if (order == null)
            {
                return Json(false);
            }
            Movie movie = order.Movies.FirstOrDefault(p => p.ID == movieId);

            if (movie != null)
            {
                return Json(false);
            }
            movie = db.Movies.FirstOrDefault(p => p.ID == movieId);
            if (movie == null)
            {
                return Json(false);
            }

            order.Movies.Add(movie);
            db.SaveChanges();
            return Json(new { Price = movie.Price, Title = movie.Title });
        }

        public ActionResult Cart()
        {
            List<Movie> order = new List<Movie>();

            int total = 0;

            if (System.Web.HttpContext.Current.Session["Cart"] != null)
            {
                foreach (var item in (List<int>)System.Web.HttpContext.Current.Session["Cart"])
                {
                    var movie = db.Movies.Where(a => a.ID == item).FirstOrDefault();
                    if (movie != null)
                    {
                        order.Add(movie);
                        total += (int)movie.Price;
                    }
                }
            }

            ViewBag.Total = total;
            ViewBag.branches = db.Branches.ToList();
            return View(order);
        }

        public int CartNumber()
        {
            List<int> Cart = (List<int>)System.Web.HttpContext.Current.Session["Cart"];
            if (Cart != null)
            {
                return Cart.Count();
            }
            else
            {
                return 0;
            }
        }
        [HttpPost]
        public JsonResult AddToCart(int movieId)
        {
            List<int> Cart = (List<int>)System.Web.HttpContext.Current.Session["Cart"];

            if (Cart == null)
            {
                Cart = new List<int>();
                System.Web.HttpContext.Current.Session["Cart"] = Cart;
            }
            if (!Cart.Contains(movieId))
            {
                Cart.Add(movieId);

                var movie = db.Movies.Single(p => p.ID == movieId);
                movie.IsCart = true;
                db.SaveChanges();         
            }
                
            return Json(true);
        }

        public JsonResult DeleteFromCart(int movieId = 0)
        {
            List<int> cart = (List<int>)System.Web.HttpContext.Current.Session["Cart"];
            if (cart != null)
            {
                cart.Remove(movieId);

                var movie = db.Movies.Single(p => p.ID == movieId);

                movie.IsCart = false;
                db.SaveChanges();

                return Json(cart.Count);
            }
            else
                return Json(0);
        }

        public JsonResult Pay(int branchId)
        {
            if (System.Web.HttpContext.Current.Session["Customer"] == null)
            {
                return Json(false);
            }
            else
            {
                Order order = new Order
                {
                    CustomerId = ((Customer)System.Web.HttpContext.Current.Session["Customer"]).CustomerID,
                    BranchID = branchId,
                    OrderDate = DateTime.Now,
                    Movies = new List<Movie>()
                };

                foreach (var item in (List<int>)System.Web.HttpContext.Current.Session["Cart"])
                {
                    
                    var movie = db.Movies.Single(p => p.ID == item);
                    movie.IsCart = false;
                    order.Movies.Add(movie);                    

                }

                db.Orders.Add(order);
                db.Customers.Find(order.CustomerId).Orders.Add(order);
                
                db.SaveChanges();

                System.Web.HttpContext.Current.Session["Cart"] = null;
                return Json(true);
            }
        }

        public ActionResult OrdersByBranch(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var OrdersByBranch = from o in db.Orders
                                 join b in db.Branches on
                                    o.BranchID equals b.BranchID
                                 where b.BranchID == id
                                 select new BranchOrdersView
                                 {
                                     branchId = o.BranchID,
                                     branchName = b.BranchName,
                                     branchCity = b.BranchCity,
                                     orderDate = o.OrderDate,
                                     orderID = o.OrderID,
                                 };

            return View(OrdersByBranch);
        }

        public ActionResult GroupByMonth()
        {
            var groupResult = db.Orders.GroupBy(b => b.OrderDate.Month).Select(g => new OrderMonthsViewModel { Month = g.Key, PostCount = g.Count() });
            ViewBag.Months = groupResult.ToList();
            return View(groupResult.ToList());
        }
        public ActionResult GenreGraph()
        { 
            var orders = db.Orders.ToList();

            List<OrderGenreViewModel> ordersArray = new List<OrderGenreViewModel>();
            Genre[] GenreArray = new Genre[11];
            
            for (var x = 0; x < orders.Count() ;x++)
            {
                var OrderMovies = orders[x].Movies.ToList();
                for (var y = 0; y < OrderMovies.Count(); y++)
                {
                    GenreArray[(int)OrderMovies[y].Genre] += 1;
                }
            }
            for(var x = 0; x < GenreArray.Count(); x++)
            {
                ordersArray.Add(new OrderGenreViewModel { Genre = x, PostCount = (int)GenreArray[x] });
            }
            ViewBag.Genre = ordersArray.ToList();
            return View(ordersArray.ToList());
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
