using System.ComponentModel.DataAnnotations;
using HightechAngular.Orders.Base;

namespace HightechAngular.Orders.Handlers
{
    public class DisputeOrder : ChangeOrderStateCommandBase
    {
        [Required]
        public string Complaint { get; set; }
    }
}