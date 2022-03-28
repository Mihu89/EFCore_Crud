using EF_Crud.Entities;
using System;
using System.Linq;

namespace EF_Crud
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UseEF();
            Console.WriteLine("Done/End");
            Console.Read();
        }

        private static void UseEF()
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
    }
}
