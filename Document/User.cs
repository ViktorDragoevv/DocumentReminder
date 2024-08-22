using Document.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Document
{
    public class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string TwoFactorEnabled { get; set; }
        public string LockoutEnd { get; set; }
        public string LockoutEnabled { get; set; }
        public string AccessFailedCount { get; set; }

        public virtual ICollection<DocumentModel>? Documents { get; set; }
    }
}