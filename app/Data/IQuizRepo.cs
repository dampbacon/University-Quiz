using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quiz.Models;
namespace quiz.Data
{
  

   
    
    public interface IQuizRepo
    {
        public IEnumerable<Post> GetAllPosts();

        public Post GetPostById(int id);

        public bool PostExists(int id);

        public IEnumerable<User> GetUsers();
        public User GetUserById(string UserName);
        public bool UserExists(string UserName);
        public bool ValidLogin(string userName, string password);
        public IEnumerable<Admin> GetAdmins();
            
        public IEnumerable<SuspendedUser> GetSuspendedUsers();
        
        public bool IsSuspended(string UserName);

        public void addSuspension(SuspendedUser badUser);
        public Post addPost(Post post);
    }




}
