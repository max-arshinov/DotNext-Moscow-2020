namespace HightechAngular.Orders.Entities
{
    public enum OrderStatus: byte
    {
        New,
        Paid,
        Shipped,
        Complete,
        Disputed
    }
}