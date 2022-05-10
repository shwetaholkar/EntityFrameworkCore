using EntityFrameworkCore.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.DataAccess
{
    public class SampleStoreContext : DbContext
    {
        //Dbset is object representation of table
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-ERGIE03\MSSQLSERVER01;Initial Catalog=SampleStore;Integrated Security=True");
        }
    }
}
