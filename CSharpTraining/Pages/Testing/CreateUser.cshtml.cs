using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CSharpData.Models;
using CSharpTraining.Utilities;
using CSharpTraining.ViewModels;
using System.ComponentModel.DataAnnotations;
using CSharpData.Models.UserTab;
using Microsoft.EntityFrameworkCore;

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
        [BindProperty]
        public string Error { get; set; } = "";
        public string Mess { get; set; } = "";
        public int ListCount { get; set; } = 0;
        [BindProperty]
        public CreateUserVM CreateUserVM { get; set; } = new CreateUserVM();
        public List<CreateUserVM> UserList { get; set; } = new List<CreateUserVM>();
        public async Task<IActionResult> OnGetAsync (string suc)
        {
            UserList = await _context.CreateUserTabs
                .Select(users => new CreateUserVM 
                {
                    Id=users.Id,
                    Name = users.Name,
                    Address = users.Address,
                    PhoneNumber = users.PhoneNumber,
                    Gender= users.Gender,
                    Marital=users.Marital,
                    DateOfBirth = users.DateOfBirth,
                    DateCreated = users.DateCreated.ToString("dd/MM/yyyy"),
                    UniqueCode=users.UniqueCode
                }).ToListAsync();
            if (UserList.Count > 0) ListCount = 1;
            CreateUserVM.UniqueCode = GenerateRandomNumbers.RandomString(10);
            CreateUserVM.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
            if (suc == "" || suc == null)
                return Page();
            else
            {
                Mess = suc;
                return Redirect("./CreateUser");
            }
        }
        public async Task<IActionResult> OnPostAsync(int id, int? ids)
        {
            int suc = 0;
            if (id == 1)
            {
                if (!ModelState.IsValid)
                {
                    Error = "Error occurred";
                    return Page();
                }
                else
                {
                    var userDetails = (dynamic)null;
                    if (CreateUserVM.Id == null)
                        userDetails = new CreateUserTab();
                    else
                        userDetails = await _context.CreateUserTabs.Where(xx => xx.Id == CreateUserVM.Id)
                            .FirstOrDefaultAsync();

                    userDetails.UniqueCode = CreateUserVM.UniqueCode;
                    userDetails.DateCreated = Convert.ToDateTime(CreateUserVM.DateCreated);
                    userDetails.Name = CreateUserVM.Name;
                    userDetails.Marital = CreateUserVM.Marital;
                    userDetails.PhoneNumber = CreateUserVM.PhoneNumber;
                    userDetails.Address = CreateUserVM.Address;
                    userDetails.DateOfBirth = CreateUserVM.DateOfBirth;
                    userDetails.Gender = CreateUserVM.Gender;

                    if (CreateUserVM.Id == null)
                        _context.CreateUserTabs.Add(userDetails);
                    else
                        _context.CreateUserTabs.Update(userDetails);

                    suc = await _context.SaveChangesAsync();
                }
                if (suc > 0) Mess = "User Successfully Created";
                else Mess = "An Error Occurred";
            }
            else
            {
                var user = await _context.CreateUserTabs.Where((user => user.Id == ids)).FirstOrDefaultAsync();
                _context.CreateUserTabs.Remove(user);
                suc = await _context.SaveChangesAsync();
                if (suc > 0) Mess = "User Successfully Deleted";
                else Mess = "An Error Occurred";
            }
            return Redirect("./CreateUser?suc="+ Mess);
        }
    }
}
