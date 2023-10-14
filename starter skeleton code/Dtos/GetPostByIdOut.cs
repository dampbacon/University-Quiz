namespace quiz.Dtos
{
    public class GetPostByIdOut
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Writer { get; set; }
        public string Time { get; set; }
        public int LikesCount { get; set; }
        public int IdOfRepliedPost { get; set; }
    }
}
