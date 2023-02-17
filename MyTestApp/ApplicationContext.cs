using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace MyTestApp
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons => Set<Person>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=MyAppDatabase.db");
        }

        public void CreateDB()
        {
            Database.EnsureCreated();
            Console.WriteLine("Database created");
        }
    }
}
