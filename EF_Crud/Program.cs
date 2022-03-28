using EF_Crud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace EF_Crud
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //UseEF();
            UseEFWithAppSettings();
            Console.Read();
        }

        private static void UseEFWithAppSettings()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();
            // read keys from file with settings
            var secret = config["mystr"];
            Console.WriteLine(secret);
            var age = config["age"];
            Console.WriteLine(age);

            // read co n ection string

            string connectionString = config.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            using (ApplicationDbContext dbContext = new ApplicationDbContext(options))
            {
                var users = dbContext.Users.ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id} {user.Name} {user.Age}");
                }
            }
        }

        /*private static void UseEF()
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                //// create new users
                //var girl = new User() { Name = "Cleopatra", Age = 23 };
                //var boy = new User() { Name = "Cesar", Age = 32 };

                //// add user into db (insert)
                //dbContext.Users.Add(girl);
                //dbContext.Users.Add(boy);

                // get all users
                var users = dbContext.Users.ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Id} {user.Name} {user.Age}");
                }
                ///Update (get user)

                var cleo = dbContext.Users.FirstOrDefault(x => x.Name == "Cleopatra");
                cleo.Age = 21;

                // Delete
                User userToDelete = dbContext.Users.FirstOrDefault(x => x.Id == 1020);
                dbContext.Users.Remove(userToDelete);

                dbContext.SaveChanges();

            }
        }
        */
    }
}
