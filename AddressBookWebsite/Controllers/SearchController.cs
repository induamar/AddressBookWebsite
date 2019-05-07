using AddressBookWebsite.Models;
using System.Linq;
using System.Web.Mvc;

namespace AddressBookWebsite.Controllers
{
    public class SearchController : Controller
    {
        private ContactsDbContext db = new ContactsDbContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult Find(string searchBy, string search)
        {
            var baseUri = Request.Url.AbsoluteUri;
            if (baseUri.Contains("Search"))
            {
                if (ModelState.IsValid)
                {
                    if (Session["RoleName"].ToString() == "Administrator" || Session["RoleName"].ToString() == "Member")
                    {
                        if (searchBy == "Gender")
                        {
                            return View(db.ContactDetails.Where(x => x.Gender.StartsWith(search) || search == null).ToList());
                        }

                        else
                        {
                            return View(db.ContactDetails.Where(x => x.FullName.StartsWith(search) || search == null).ToList());
                        }
                    }

                    else
                    {
                        return RedirectToAction("ErrorPage", "Contacts");
                    }
                }
            }
            return View();
        }
    }
}
