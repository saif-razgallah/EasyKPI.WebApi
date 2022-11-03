using EasyKPI.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyKPI.Core.DTO
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public string Token { get; set;}
        public string Username { get; set; }
    }
}
