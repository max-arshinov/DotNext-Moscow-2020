using System.ComponentModel.DataAnnotations;
using HightechAngular.Orders.Base;

namespace HightechAngular.Orders.Handlers
{
    public class DisputeOrder : ChangeOrderStateCommandBase
    {
        [Required]
        public string Complaint { get; set; } =
            "Complaint text has to be set on UI. It's hardcoded for the demo purposes only";
    }
}