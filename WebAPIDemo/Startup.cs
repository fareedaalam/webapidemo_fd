﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using BusinessServices;

[assembly: OwinStartup(typeof(WebAPIDemo.Startup))]

namespace WebAPIDemo
{
    public partial class Startup
    {
        private static object Mapper;

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
