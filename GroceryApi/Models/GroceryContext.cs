//using System;
using Microsoft.EntityFrameworkCore;

namespace GroceryApi.Models
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions<GroceryContext> options)
            : base(options)
        {
        }
        public DbSet<GroceryItem> GroceryItems { get; set; }
    }
}



//This is the database context- the main class that coordinates
//Entity Framework functionality for a data model

//In ASP.NET Core, services such as Db context must be registered
//with the dependnecy injection container to provide the service to
//controllers