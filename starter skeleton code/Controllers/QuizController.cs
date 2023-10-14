using Microsoft.AspNetCore.Mvc;
using quiz.Models;
using quiz.Dtos;

namespace quiz.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizController : Controller
    {
        [HttpGet("GetVersion")]
        public ActionResult<string> GetVersion()
        {
            return Ok();
        }


        [HttpGet("GetAllPosts")]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {

            return Ok();

        }

        [HttpGet("GetPostById/{id}")]
        public ActionResult<GetPostByIdOut> GetPostById(int id)
        {
            return Ok();
        }

        [HttpGet("GetFollowing/{userName}")]
        public ActionResult<IEnumerable<string>> GetFollowing(string userName)
        {
            return Ok();
        }

        [HttpGet("GetFollower/{userName}")]
        public ActionResult<IEnumerable<string>> GetFollower(string userName)
        {
            return Ok();
        }

        [HttpPost("SuspendUser")]
        public ActionResult<string> SuspendUser(SuspendedUser suspendedUser)
        {
            return Ok();
        }
        

        [HttpPost("PostMessage")]
        public ActionResult<GetPostByIdOut> PostMessage(PostInput message)
        {
            return Ok();
        }
 
        [HttpGet("GetRecommendedUsers")]
        public ActionResult<IEnumerable<string>> GetRecommendedUsers()
        {

            return Ok();
        }

    }
}
