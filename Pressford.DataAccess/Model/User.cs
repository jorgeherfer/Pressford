namespace Pressford.DataAccess.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
    }
}