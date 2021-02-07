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
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> logger;
        private readonly NotesContext db;

        public NoteController(ILogger<NoteController> logger, NotesContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult CreateNote()
        {
            User checkUser = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(checkUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult ToCreateNote(string title, string body)
        {
            User checkUser = HttpContext.Session.Get<User>("UsuarioLogueado");
            User userToAddNote = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Email.Equals(checkUser.Email));

            if(title == null)
            {
                Note newNote = new Note{
                    Body = body,
                    Creator = checkUser,
                };
                
                userToAddNote.Notes.Add(newNote);
                db.Users.Update(userToAddNote);
                db.SaveChanges();

                if(userToAddNote.Notes.Count() > 0)
                {
                    return RedirectToAction ("Index", "Home", userToAddNote.Notes.ToList());
                }
                else
                {
                    ViewBag.EmptyNotes = true;
                    return View();
                }
            }
            else
            {
                Note newNote = new Note{
                    Title = title,
                    Body = body,
                    Creator = checkUser
                };

                userToAddNote.Notes.Add(newNote);
                db.Users.Update(userToAddNote);
                db.SaveChanges();

                if(userToAddNote.Notes.Count() > 0)
                {
                    return RedirectToAction ("Index", "Home", userToAddNote.Notes.ToList());
                }
                else
                {
                    ViewBag.EmptyNotes = true;
                    return View();
                }
            }
        }

        public void DeleteTweet(int ID)
        {
            Note noteToSearch = db.Notes.FirstOrDefault(n => n.NoteID == ID);

            if(noteToSearch != null)
            {
                noteToSearch.Status = 1;
                db.Notes.Update(noteToSearch);
                db.SaveChanges();
            }
        }
        
        public IActionResult DeleteNote()
        {
            User checkUser = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(checkUser != null)
            {
                User userToBringNotes = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Email.Equals(checkUser.Email));

                if(userToBringNotes.Notes.Count() > 0)
                {
                    return View(userToBringNotes.Notes.ToList());
                }
                else
                {
                    ViewBag.EmptyNotes = true;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public void DefinitelyDelete(int ID)
        {
            Note noteToDelete = db.Notes.FirstOrDefault(n => n.NoteID == ID);

            if(noteToDelete != null)
            {
                db.Notes.Remove(noteToDelete);
                db.SaveChanges();
            }
        }

        public void RestoreNote(int ID)
        {
            Note noteToRestore = db.Notes.FirstOrDefault(n => n.NoteID == ID);

            if(noteToRestore != null)
            {
                noteToRestore.Status = 0;
                db.Notes.Update(noteToRestore);
                db.SaveChanges();
            }
        }

        public void ToArchiveNote(int ID)
        {
            Note noteToArchive = db.Notes.FirstOrDefault(n => n.NoteID == ID);

            if(noteToArchive != null)
            {
                noteToArchive.Status = 2;
                db.Notes.Update(noteToArchive);
                db.SaveChanges();
            }
        }

        public void UnarchiveNote(int ID)
        {
            Note noteToUnarchive = db.Notes.FirstOrDefault(n => n.NoteID == ID);

            if(noteToUnarchive != null)
            {
                noteToUnarchive.Status = 0;
                db.Notes.Update(noteToUnarchive);
                db.SaveChanges();
            }
        }

        public IActionResult ArchiveNote()
        {
            User checkUser = HttpContext.Session.Get<User>("UsuarioLogueado");
            
            if(checkUser != null)
            {
                User userToBringNotes = db.Users.Include(u => u.Notes).FirstOrDefault(u => u.Email.Equals(checkUser.Email));

                if(userToBringNotes.Notes.Count() > 0)
                {
                    return View(userToBringNotes.Notes.ToList());
                }
                else
                {
                    ViewBag.EmptyNotes = true;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
