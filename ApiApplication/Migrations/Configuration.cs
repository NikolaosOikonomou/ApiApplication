namespace ApiApplication.Migrations
{
    using ApiApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApiApplication.MyApp.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApiApplication.MyApp.ApplicationDbContext context)
        {
          
        }
    }
}
