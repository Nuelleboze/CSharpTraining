using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpData.Models.UserTab
{
    public class CreateUserTab
    {
        public int Id { get; set; }
        [MaxLength(30)]
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
    }
}
