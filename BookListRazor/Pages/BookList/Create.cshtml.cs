using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _appdbcontext;

        //Dependency Injection DB
        public CreateModel(ApplicationDbContext AppDbContext)
        {
            _appdbcontext = AppDbContext;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _appdbcontext.Books.Add(Book);
            await _appdbcontext.SaveChangesAsync();

            Message = "Book has been created successfully";
            return RedirectToPage("Index");
        }
    }
}