using System.ComponentModel.DataAnnotations;
using HightechAngular.Orders.Base;

namespace HightechAngular.Shop.Features.MyOrders
{
    public class DisputeOrder : ChangeOrderStateBase
    {
        [Required]
        public string Complaint { get; set; } = 
            "Complaint Text should be sent from UI. It's hardcoded here for demo purposes only";
    }
}