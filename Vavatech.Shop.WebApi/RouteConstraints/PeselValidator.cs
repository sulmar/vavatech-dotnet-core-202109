namespace Vavatech.Shop.WebApi.RouteConstraints
{
    public class PeselValidator : ValidatorBase
    {
        protected override byte[] Weights => new byte[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        protected override int CheckControl(int sumControl) => 10 - sumControl % 10;
    }
}
