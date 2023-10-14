using Microsoft.EntityFrameworkCore.ChangeTracking;
using quiz.Models;
namespace quiz.Data
{
    public class QuizRepo: IQuizRepo
    {

        private readonly QuizDBContext _dbContext;

        public QuizRepo(QuizDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _dbContext.Posts;
        }

        public Post GetPostById(int id)
        {
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool PostExists(int id)
        {
            return _dbContext.Posts.Any(p => p.Id == id);
        }



        public User GetUserById(string UserName)
        {
            return _dbContext.Users.FirstOrDefault(p => p.UserName == UserName);
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users;
        }

        public bool UserExists(string UserName)
        {
            return _dbContext.Users.Any(p => p.UserName == UserName);
        }

        public bool ValidLogin(string userName, string password)
        {
            User c = _dbContext.Users.FirstOrDefault(e => e.UserName == userName && e.Password == password);

            if (c == null) {
                Admin a = _dbContext.Admins.FirstOrDefault(p => p.UserName == userName && p.Password == password);
                if (a == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return true;
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return _dbContext.Admins;
        }

        public IEnumerable<SuspendedUser> GetSuspendedUsers()
        {
            return _dbContext.SuspendedUsers;
        }

        public bool IsSuspended(string UserName)
        {
            return _dbContext.SuspendedUsers.Any(p => p.UserName == UserName);
        }

        public void addSuspension(SuspendedUser badUser)
        {
            _dbContext.SuspendedUsers.Add(badUser);
            _dbContext.SaveChanges();
        }

        public Post addPost(Post post)
        {


            EntityEntry<Post> e = _dbContext.Posts.Add(post);
            Post p = e.Entity;
            _dbContext.SaveChanges();
            return p;
        }
    }
}
