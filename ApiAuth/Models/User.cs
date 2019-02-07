using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAuth.Models
{
    public class User
    {
        public UserRole Role { get; set; }
        public void AddRole(UserRole userRole)
        {
            this.Role |= userRole;
        }
        public void RemoveRole(UserRole userRole)
        {
            this.Role &= ~userRole;
        }
        public bool IsInRole(UserRole userRole)
        {
            return this.Role.HasFlag(userRole);
        }
    }
}