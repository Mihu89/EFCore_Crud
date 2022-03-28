using EF_Crud.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Crud
{
    public class ApplicationDbContext : DbContext
    {
        private readonly StreamWriter logStream = new StreamWriter("myLogs.txt", true);
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Server=.;Database=UserDB;Trusted_connection=True;");
            // use for logging of sql changes
            //dbContextOptionsBuilder.LogTo(System.Console.WriteLine); // use for logging in console
            dbContextOptionsBuilder.LogTo(System.Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name}); // use for logging in console
            //dbContextOptionsBuilder.LogTo(logStream.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning);
        }

        public override void Dispose()
        {
            base.Dispose();
            logStream.Dispose();
        }
        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            await logStream.DisposeAsync();
        }
        //Problem with paranthesys
        // {[()]} [()()]
        // [(]
    }
}
