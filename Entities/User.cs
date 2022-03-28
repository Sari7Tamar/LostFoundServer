using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
        public bool IsManager { get; set; }
        public string Fhone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
