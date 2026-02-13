namespace ProductWebApi.DTO
{
    public class RegisterDTO
    {
        public string UserName { get; set; }= null!; 

        public string UserEmail { get; set; }= null!; 

        public string Password { get; set; } = null!;

        public string AccountType { get; set; } = null!; 

    }
}
