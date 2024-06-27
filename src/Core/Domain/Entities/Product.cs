public class Product
{
    public string UserId { get; set; }
    public string ProductNo { get; set; }
    public string PolicyNo { get; set; }
    public float Premium { get; set; }
    public string Insured { get; set; }
    public string Plate { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Statu { get; set; }
    public Product(string userId, string productNo, string policyNo, float premium, string insured, string plate, string statu)
    {
        UserId = userId;
        ProductNo = productNo;
        PolicyNo = policyNo;
        Premium = premium;
        Insured = insured;
        Plate = plate;
        CreatedDate = DateTime.Now;
        Statu = statu;
    }
}