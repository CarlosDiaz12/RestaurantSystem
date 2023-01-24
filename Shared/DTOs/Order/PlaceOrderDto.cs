namespace RestaurantSystem.Shared.DTOs.Order
{
    public class PlaceOrderDto
    {
        public int TableId { get; set; }
        public string ClientName { get; set; }
        public string WaiterUserId { get; set; }
        public string CheffUserId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
