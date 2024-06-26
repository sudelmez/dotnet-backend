namespace TodoApi2.src.Core.Domain.Entities
{
    public class Log
    {
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Message { get; set; }
        public string IpAdress { get; set; }
        public string UserAgent { get; set; }
    }
}