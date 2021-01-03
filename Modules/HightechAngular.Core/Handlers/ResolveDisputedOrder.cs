using System.ComponentModel.DataAnnotations;
using HightechAngular.Orders.Base;

namespace HightechAngular.Orders.Handlers
{
    public class ResolveDisputedOrder : ChangeOrderStateCommandBase
    {
        [Required]
        public string ResolutionComment { get; set; } = 
            "Resolution text has to be set on UI. It's hardcoded for the demo purposes only";
    }
}