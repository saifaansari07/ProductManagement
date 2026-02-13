namespace ProductWebApi.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;

        public string Username { get; set; }=null!;

        public List<string> Roles  { get; set; }=new List<string>();
    }
}
