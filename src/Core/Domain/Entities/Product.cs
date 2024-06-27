using TodoApi2.src.Core.Domain.Entities;

public class ProductEntity
{
    public string UserId { get; set; }
    public string ProductNo { get; set; }
    public string PolicyNo { get; set; }
    public float Premium { get; set; }
    public string Insured { get; set; }
    public string Plate { get; set; }
    public DateTime CreatedDate { get; set; }
    public ProductStatus Statu { get; set; }
    protected ProductEntity() { } //orm maplemesi sağlanacak
    public ProductEntity(string userId, string productNo, string policyNo, float premium, string insured, string plate, DateTime date)
    {
        UserId = userId;
        ProductNo = productNo;
        PolicyNo = policyNo;
        Premium = premium;
        Insured = insured;
        Plate = plate;
        CreatedDate = date;
        Statu = ProductStatus.Opened;
        //statusu buradan statik verdim enumdan çağırıp
    }
}