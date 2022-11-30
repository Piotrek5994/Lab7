using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_6.Models;
using lab_6.Interfejs;

namespace lab_6.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(AppDbContext context, IAuthorService authorService)
        {
            _authorService = authorService;
        }
       
        public IActionResult Index()
        {
            return View(_authorService.FindAll());
        }

        public IActionResult Details(int? id)
        {
            var book = _authorService.FindBy(id);
            return book is null ? NotFound() : View(book);
        }
        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,PESEL")] Author author)
        {
            if (ModelState.IsValid)
            {
                _authorService.Save(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public IActionResult Edit(int? id)
        {
            var book = _authorService.FindBy(id);
            return book is null ? NotFound() : View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,PESEL")] Author author)
        {
            if (ModelState.IsValid)
            {
                _authorService.Update(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = _authorService.FindBy(id);
            return book is null ? NotFound() : View(book);
        }
        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_authorService.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }
            return Problem("Trying delete no existing author");
        }
        private readonly AppDbContext _context;

       
    }
}
