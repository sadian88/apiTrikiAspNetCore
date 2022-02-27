using System;
using System.Collections.Generic;
using System.Text;

namespace Triki.CI.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string LastName { get; set; }
        public int TipoIdenID { get; set; }
        public int IndentityNumber { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
