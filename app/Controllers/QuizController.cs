using Microsoft.AspNetCore.Mvc;
using quiz.Models;
using quiz.Dtos;
using quiz.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace quiz.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizController : Controller
    {

        private readonly IQuizRepo _repository;

        public QuizController(IQuizRepo repository)
        {
            _repository = repository;
        }

        [HttpGet("GetVersion")]
        public ActionResult<string> GetVersion()
        {
            return Ok("V893411218QZ");
        }


        [HttpGet("GetAllPosts")]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {

            return Ok(_repository.GetAllPosts());

        }

        [HttpGet("GetPostById/{id}")]
        public ActionResult<GetPostByIdOut> GetPostById(int id)
        {
            if (_repository.PostExists(id))
            {
                Post temp= _repository.GetPostById(id);
                int likesCount = 0;
                if (temp.Likes != null)
                {
                    likesCount = temp.Likes.Split(",").Count();
                }

                GetPostByIdOut post= new GetPostByIdOut { Content=temp.Content, Id= temp.Id, 
                    IdOfRepliedPost=temp.IdOfRepliedPost, LikesCount=likesCount, Time=temp.Time, Writer=temp.Writer};
                return Ok(post);
            }
            else
            {
                return BadRequest("Post with Id "+ id.ToString()+ " does not exist");
            }
        }

        [HttpGet("GetFollowing/{userName}")]
        public ActionResult<IEnumerable<string>> GetFollowing(string userName)
        {
            if (_repository.UserExists(userName))
            {
                List<string> emptyList = new List<string>();
                User user= _repository.GetUserById(userName);
                if (user.Following != null)
                {
                    return Ok(_repository.GetUserById(userName).Following.Split(","));
                }
                else
                {
                    return emptyList;
                }
            }
            else
            {
                return BadRequest("UserName "+userName+" does not exist");
            };
        }

        [HttpGet("GetFollower/{userName}")]
        public ActionResult<IEnumerable<string>> GetFollower(string userName)
        {
            if (_repository.UserExists(userName))
            {
                IEnumerable<User> allUsers = _repository.GetUsers();
                IEnumerable<User> noNullFollowersUsers = allUsers.Where(u=>u.Following!= null);

                if (!noNullFollowersUsers.Any())
                {
                    return Ok(new List<string>());
                }

                User user = _repository.GetUserById(userName);
                IEnumerable<string> FollowersList = new List<string>();
                //allUsers.GroupBy(u => u.Following.Split(",").Contains(userName));

                IEnumerable<User> followers = noNullFollowersUsers.Where(u => u.Following.Contains(userName));

                var followerNames = new List<string>();

                foreach (User item in followers)
                {
                    followerNames.Add(item.UserName);
                    Console.WriteLine(item.UserName);
                }

                return Ok(followerNames);
            }
            else
            {
                return BadRequest("UserName " + userName + " does not exist");
            };
        }


        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("SuspendUser")]
        public ActionResult<string> SuspendUser(SuspendedUser suspendedUser)
        {
            if (suspendedUser.Reason==null || suspendedUser.UserName == null)
            {
                return BadRequest();
            }
            if (_repository.IsSuspended(suspendedUser.UserName))
            {
                return Ok("The user has already been suspended.");
            }else if (_repository.UserExists(suspendedUser.UserName)) //username check if valid for new suspension
            {
                _repository.addSuspension(suspendedUser);
                return Ok("Success");
            }
            else
            {
                return BadRequest("UserName "+suspendedUser.UserName+" does not exist.");
            }
        }

        [Authorize(AuthenticationSchemes = "Authentication")]
        [Authorize(Policy = "AuthOnly")]
        [HttpPost("PostMessage")]
        public ActionResult<GetPostByIdOut> PostMessage(PostInput message)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("normalUser");
            Console.WriteLine("###############################################");
            Console.WriteLine(c.Value);
            DateTime time= DateTime.UtcNow;
            if (_repository.IsSuspended(c.Value))
            {
                return BadRequest("You cannot carry out this operation as your account is suspended.");
            }
            if (_repository.PostExists(message.IdOfRepliedPost))
            {
                Post newPost = new Post { Content = message.Message, IdOfRepliedPost = message.IdOfRepliedPost, Writer = c.Value, Time = time.ToString("yyyy-MM-dd HH:mm:ss") };
                Post post = _repository.addPost(newPost);
                GetPostByIdOut a = new GetPostByIdOut { Id = post.Id, Content = post.Content, IdOfRepliedPost = post.IdOfRepliedPost, LikesCount = 0, Time = post.Time, Writer = post.Writer };
                return CreatedAtAction(nameof(GetPostById), new { id = a.Id }, a);
            }
            else if (message.IdOfRepliedPost != 0 || message.IdOfRepliedPost == null)
            {
                Post newPost = new Post { Content = message.Message, IdOfRepliedPost = 0, Writer = c.Value, Time = time.ToString("yyyy-MM-dd HH:mm:ss") };
                Post post = _repository.addPost(newPost);
                GetPostByIdOut a = new GetPostByIdOut {Id=post.Id, Content=post.Content, IdOfRepliedPost=post.IdOfRepliedPost, LikesCount = 0, Time=post.Time, Writer=post.Writer};
                return CreatedAtAction(nameof(GetPostById), new { id = a.Id }, a);
            }
            else
            {
                return BadRequest("Post " + message.IdOfRepliedPost.ToString() + " does not exist");
            }
        }
 
        [HttpGet("GetRecommendedUsers")]
        public ActionResult<IEnumerable<string>> GetRecommendedUsers()
        {

            return Ok();
        }

    }
}
