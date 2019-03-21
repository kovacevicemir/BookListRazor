using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }
        public void OnGet(int id)
        {
            Book = _db.Books.Find(id);
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var BookFromDb = await _db.Books.FindAsync(Book.Id);

            BookFromDb.Name = Book.Name;
            BookFromDb.Author = Book.Author;
            BookFromDb.ISBN = Book.ISBN;

            await _db.SaveChangesAsync();

            Message = "Book has been updated successfully";
            return RedirectToPage("Index");
        }

        //ON POST - OTHER WAY AROUND
        //public async Task<IActionResult> OnPost([Bind("Id,Name,Author,ISBN")] Book book)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _db.Update(book);
        //    await _db.SaveChangesAsync();
        //    return RedirectToPage("Index");
        //}


    }
}