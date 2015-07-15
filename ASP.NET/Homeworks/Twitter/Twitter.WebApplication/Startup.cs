﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twitter.WebApplication.Startup))]
namespace Twitter.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
