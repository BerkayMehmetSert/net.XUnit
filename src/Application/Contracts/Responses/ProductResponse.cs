namespace Application.Contracts.Responses;

public class ProductResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}