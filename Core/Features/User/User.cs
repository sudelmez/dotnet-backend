namespace TodoApi2;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
    public string Client { get; set; }
    public List<string> AuthorizedProducts { get; set; }
    public string CreatedDate { get; set; }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
