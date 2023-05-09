using ProjectCuoiki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace ProjectCuoiki.Controllers
{
    public class RegisterController : Controller
    {
        ProjectCuoiKiEntities db = new ProjectCuoiKiEntities();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(userAccount user)
        {
            var check = db.userAccounts.FirstOrDefault(s => s.userName == user.userName);
            userRole userrole = new userRole();
            string message = string.Empty;

            if (check == null)
            {
                try
                {
                    user.passWord = GetMD5(user.passWord);
                    user.repassWord = GetMD5(user.repassWord);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.userAccounts.Add(user);
                    db.SaveChanges();

                    userrole.userID = user.userID;
                    userrole.role = "User";
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.userRoles.Add(userrole);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

            }
            else
            {
                message = "Username is exists";
            }
            ViewBag.Message = message;
            return RedirectToAction("Index", "Login");
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

    }
}