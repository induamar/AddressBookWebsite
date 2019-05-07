using AddressBookWebsite.Models;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AddressBookWebsite.Controllers
{
    public class ContactsController : Controller
    {
        private ContactsDbContext db = new ContactsDbContext();
        private string avatar = "~/Avatar/";

        public ActionResult Index()
        {
            if (Session["FullName"] != null)
            {
                return View(db.ContactDetails.ToList());
            }

            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactViewModel contacts = db.ContactDetails.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ErrorPage()
        {
            return View();
        }

        public ActionResult ErrorPageEdit()
        {
            return View();
        }

        public ActionResult ErrorPageDelete()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactID, Avatar, FullName, Age, Gender, AddressOne, AddressTwo,Phone,Mobile,Email")] ContactViewModel model,FormCollection fc, HttpPostedFileBase imageOne)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                if (ModelState.IsValid)
                {
                    var user = db.ContactDetails.FirstOrDefault(u => u.Avatar == avatar + imageOne.FileName || u.Email == model.Email || u.FullName == model.FullName);
                    if (user != null)
                    {
                        ModelState.AddModelError("", "This Contact already exists");
                    }

                    else if (imageOne == null)
                    {
                        ModelState.AddModelError("", "Please select a photo");
                    }

                    else if (imageOne != null)
                    {
                        string fileName = Path.GetFileName(imageOne.FileName);
                        FileInfo file = new FileInfo(fileName);

                        if (file.Extension == ".jpg" || file.Extension == ".jpeg" || file.Extension == ".png")
                        {
                            var filePath = Path.GetFileName(imageOne.FileName);
                            var path = Path.Combine(Server.MapPath(avatar), filePath);
                            imageOne.SaveAs(path);

                            Image image = Image.FromFile(path);

                            if(imageOne.ContentLength > 1048576)
                            {
                                ModelState.AddModelError("", "Please select only a photo which is PNG, JPEG or JPG format that has a size of less than 1MB size");
                            }

                            else if (image.Width == 256 && image.Height == 256)
                            {
                                model.Avatar = avatar + filePath;
                                db.ContactDetails.Add(model);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }

                            else
                            {
                                ModelState.AddModelError("", "Please select only photos of pixel size 256 X 256 only");
                            }
                        }

                        else
                        {
                            ModelState.AddModelError("", "");
                        }
                    }
                }

                return View(model);
            }

            catch (Exception exception)
            {
                string excep = exception.Message;
            }
            return View(model);
        }

        [AllowAnonymous]
        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["RoleName"].ToString() == "Administrator" || Session["RoleName"].ToString() == "Member")
            {
                ContactViewModel contacts = db.ContactDetails.Find(id);
                if (contacts == null)
                {
                    return HttpNotFound();
                }
                return View(contacts);
            }

            else
            {
                return RedirectToAction("ErrorPageEdit", "Contacts");
            }
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactID, Avatar, FullName, Age, Gender,AddressOne,AddressTwo,Phone,Mobile,Email")] ContactViewModel contacts, BAL.ContactsBusinessLayer contactBLL, int ?id)
        {
            if (ModelState.IsValid)
            {
                contacts = db.ContactDetails.Find(id);
                var filePath = from contact in db.ContactDetails
                               where contact.ContactID == id
                               select contact;

                //Sets properties taken from linq
                var avatar = contacts.Avatar;
                var fullName = Request.Form["FullName"];
                var age = Request.Form["Age"];
                var gender = Request.Form["Gender"];
                var addressOne = Request.Form["AddressOne"];
                var addressTwo = Request.Form["AddressTwo"];
                var phone = Request.Form["Phone"];
                var mobile = Request.Form["Mobile"];
                var email = Request.Form["Email"];

                if (Session["RoleName"].ToString() == "Administrator" || Session["RoleName"].ToString() == "Member")
                {
                    contacts = new ContactViewModel()
                    {
                        //Sets Properties
                        Avatar = avatar,
                        ContactID = id,
                        FullName = fullName,
                        Age = Convert.ToInt32(age),
                        Gender = gender,
                        AddressOne = addressOne,
                        AddressTwo = addressTwo,
                        Phone = phone,
                        Mobile = mobile,
                        Email = email
                    };

                    BAL.ContactsBusinessLayer contactBusinessLayer = new BAL.ContactsBusinessLayer();
                    contactBusinessLayer.SaveContactDetails(contacts);
                    return RedirectToAction("Index");
                }

                else
                {
                    return RedirectToAction("ErrorPageEdit", "Contacts");
                }
            }
            return View(contacts);
    }

    // GET: Contacts/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        if (Session["RoleName"].ToString() == "Administrator")
        {
            ContactViewModel contacts = db.ContactDetails.Find(id);
            if (contacts == null)
            {
                return HttpNotFound();
            }
            return View(contacts);
        }

        else
        {
            return RedirectToAction("ErrorPageDelete", "Contacts");
        }
    }

    // POST: Contacts/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        if (Session["RoleName"].ToString() == "Administrator")
        {
            ContactViewModel contacts = db.ContactDetails.Find(id);
            db.ContactDetails.Remove(contacts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        else
        {
            return RedirectToAction("ErrorPageDelete", "Contacts");
        }
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
