﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebAPIDemo.Filters
{
    public class BasicAuthenticationIdentity: GenericIdentity
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid? UserId { get; set; }
       // Basic Authentication Identity Constructor
        public BasicAuthenticationIdentity(string userName, string password)
            : base(userName, "Basic")
        {
            Password = password;
            UserName = userName;
        }
    }
}