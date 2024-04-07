namespace CustomerService.Application.Dto
{
    public class CustomerLoginDto
    {
        //the login for name or email 
        private string _emailOrName;
        private string _password;

        public CustomerLoginDto(string emailOrName, string password)
        {
            _emailOrName = emailOrName;
            _password = password;
        }
      
        public string EmailOrName { get => _emailOrName; set => _emailOrName = value; }
        public string Password { get => _password; set => _password = value; }
    }
}
