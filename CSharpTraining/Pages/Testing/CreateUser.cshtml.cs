using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpData.Models;

namespace CSharpTraining.Pages.Testing
{
    public class CreateUserModel : PageModel
    {
        //Constructors
        private readonly CSharpDbContext _context;
        public CreateUserModel(CSharpDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {

        }
    }
}
