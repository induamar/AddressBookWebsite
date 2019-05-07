using AddressBookWebsite.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace AddressBookWebsite.Controllers
{
    public class HomeController : Controller
    {
        ContactsDbContext db = new ContactsDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}