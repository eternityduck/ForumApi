using System;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime MemberSince { get; set; }
    }
}