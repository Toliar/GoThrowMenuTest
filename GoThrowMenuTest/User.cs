using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserData
    {
        public string Email { get; set; } = $"@" + "test.test";
        public string Password { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

    }
}
