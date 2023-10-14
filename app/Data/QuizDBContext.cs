using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quiz.Models;
namespace quiz.Data
{
        public class QuizDBContext  : DbContext
        {
            public QuizDBContext(DbContextOptions<QuizDBContext> options) : base(options) { }
            public DbSet<Post> Posts { get; set; }
            public DbSet<User> Users { get; set; }
            
            public DbSet<Admin> Admins { get; set; }
            public DbSet<SuspendedUser> SuspendedUsers { get; set;}

        }
    

}
