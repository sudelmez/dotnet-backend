public class Users
{
    public int Id { get; set; }
    public string CreatedDate { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Client { get; set; }
    public List<string> AuthorizedProducts { get; set; }
}