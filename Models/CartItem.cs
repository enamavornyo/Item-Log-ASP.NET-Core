
namespace ItemLog.Models
{
    public class CartItem
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
        //public decimal Total
        //{
        //    get { return Quantity * Price; }
        //}
        public string ImageURL { get; set; }

        public CartItem()
        {
        }

        public CartItem(Item Item)
        {
            ItemId = Item.Id;
            ItemName = Item.Name;
            //Price = Item.Price;
            Quantity = 1;
            ImageURL = Item.ImageURL;
        }

    }
}