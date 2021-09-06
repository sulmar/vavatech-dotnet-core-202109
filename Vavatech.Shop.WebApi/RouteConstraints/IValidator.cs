namespace Vavatech.Shop.WebApi.RouteConstraints
{
    public interface IValidator
    {
        bool IsValid(string number);
    }
}
