namespace ECommerce.ViewModels.Cart
{
    public class AddToCartViewModel
    {
        public long ProductId { get; set; }
        public string CategorySlug { get; set; }
        public string BrandSlug { get; set; }
        public string Page { get; set; }
    }
}
