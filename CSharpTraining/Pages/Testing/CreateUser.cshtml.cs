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
        public async Task<IActionResult> OnGetAsync(string suc, int? id)
        {
            //checking if record has been saved

            //checking if we want to edit a saved record
            if (id == null)
            {
                UserList = await _context.CreateUserTabs
                .Select(users => new CreateUserVM
                {
                    Id = users.Id,
                    Name = users.Name,
                    Address = users.Address,
                    PhoneNumber = users.PhoneNumber,
                    Gender = users.Gender,
                    Marital = users.Marital,
                    DateOfBirth = users.DateOfBirth,
                    DateCreated = users.DateCreated.ToString("dd/MM/yyyy"),
                    UniqueCode = users.UniqueCode,
                    DateBirth = users.DateOfBirth.ToString("yyyy-MM-dd")
                }).ToListAsync();
                CreateUserVM.UniqueCode = GenerateRandomNumbers.RandomString(10);
                CreateUserVM.DateCreated = DateTime.Now.ToString("dd/MM/yyyy");
                if (UserList.Count > 0) ListCount = 1;
            }
            else
            {
                if (suc == "11")
                {
                    var user = await _context.CreateUserTabs.Where((user => user.Id == Convert.ToInt32(id))).FirstOrDefaultAsync();
                    _context.CreateUserTabs.Remove(user);
                    await _context.SaveChangesAsync();
                    UserList = await _context.CreateUserTabs
                .Select(users => new CreateUserVM
                {
                    Id = users.Id,
                    Name = users.Name,
                    Address = users.Address,
                    PhoneNumber = users.PhoneNumber,
                    Gender = users.Gender,
                    Marital = users.Marital,
                    DateOfBirth = users.DateOfBirth,
                    DateCreated = users.DateCreated.ToString("dd/MM/yyyy"),
                    UniqueCode = users.UniqueCode,
                    DateBirth = users.DateOfBirth.ToString("yyyy-MM-dd")
                }).ToListAsync();
                    if (UserList.Count > 0) ListCount = 1;
                }
                else
                {
                    CreateUserVM = await _context.CreateUserTabs.Where(xx => xx.Id == id)
                        .Select(users => new CreateUserVM
                        {
                            Id = users.Id,
                            Name = users.Name,
                            Address = users.Address,
                            PhoneNumber = users.PhoneNumber,
                            Gender = users.Gender,
                            Marital = users.Marital,
                            DateOfBirth = users.DateOfBirth,
                            DateCreated = users.DateCreated.ToString("dd/MM/yyyy"),
                            UniqueCode = users.UniqueCode,
                            DateBirth = users.DateOfBirth.ToString("yyyy-MM-dd")
                        }).FirstOrDefaultAsync();
                    if (UserList.Count > 0) ListCount = 0;
                }
            }
            if (suc == "" || suc == null || suc == "10")
                return Page();
            else
            {
                Mess = suc;
                return Redirect("./CreateUser");
            }
        }
        public async Task<IActionResult> OnPostAsync(string id)
        {
            int suc = 0;
            if (id == "sub")
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
                var user = await _context.CreateUserTabs.Where((user => user.Id == Convert.ToInt32(id))).FirstOrDefaultAsync();
                _context.CreateUserTabs.Remove(user);
                suc = await _context.SaveChangesAsync();
                if (suc > 0) Mess = "User Successfully Deleted";
                else Mess = "An Error Occurred";
            }
            return Redirect("./CreateUser?suc=" + Mess);
        }
    }
}
