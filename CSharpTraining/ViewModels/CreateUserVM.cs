using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpTraining.ViewModels
{
    public class CreateUserVM
    {
        public int? Id { get; set; }
       
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(30, ErrorMessage = "This field cannot be more than 30 characters")]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        [MaxLength(6)]
        public string Gender { get; set; }
        [MaxLength(8)]
        public string Marital { get; set; }
        [MaxLength(11)]
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(10)]
        public string UniqueCode { get; set; }
        public string DateCreated { get; set; }
        public string DateBirth { get; set; }
    }
}
