using System.ComponentModel.DataAnnotations;
using HightechAngular.Orders.Base;

namespace HightechAngular.Orders.Handlers
{
    public class CompleteOrder : ChangeOrderStateCommandBase
    {
        [Required]
        public string ResolutionComment { get; set; }
    }
}