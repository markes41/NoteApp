using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly NotesContext db;

        public HomeController(ILogger<HomeController> logger, NotesContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ToLogin(string email, string password)
        {
            User checkUser = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(checkUser == null)
            {
                checkUser  = db.Users.FirstOrDefault(u => u.Email.Equals(email));
                if(checkUser != null && checkUser.Password.Equals(password))
                {
                    HttpContext.Session.Set<User>("UsuarioLogueado", checkUser);
                    
                    User userToBringNotes = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Email.Equals(checkUser.Email));
                    if(userToBringNotes.Notes.Count() > 0)
                    {
                        return RedirectToAction("CreateNote", "Note", userToBringNotes.Notes.ToList());
                    }
                    else
                    {
                        ViewBag.EmptyNotes = true;
                        return View();
                    }
                }
                else
                {
                    ViewBag.errorCredenciales = true;
                    return RedirectToAction("CreateNote", "Note");
                }
            }
            else
            {
                User userToBringNotes = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Email.Equals(checkUser.Email));
                if(userToBringNotes.Notes.Count() > 0)
                {
                    return RedirectToAction("CreateNote", "Note", userToBringNotes.Notes.ToList());
                }
                else
                {
                    ViewBag.EmptyNotes = true;
                    return View();
                }
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult ToRegister(string email, string password, string first_name, string last_name)
        {
            User userCheck = db.Users.FirstOrDefault(u => u.Email.Equals(email));

            if(userCheck == null){
                User newUser = new User{
                Email = email,
                Password = password,
                First_Name = first_name,
                Last_Name = last_name
                };

                db.Users.Add(newUser);
                db.SaveChanges();
                return View("Login");

            }
            else
            {
                ViewBag.existeUsuario = true;
                return View("Register");
            }   
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
