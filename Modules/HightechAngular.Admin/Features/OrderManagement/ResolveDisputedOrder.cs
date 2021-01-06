using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Force.Cqrs;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ResolveDisputedOrder : ICommand<Task>
    {
        [Required]
        public string ResolutionComment { get; set; }
    }
}