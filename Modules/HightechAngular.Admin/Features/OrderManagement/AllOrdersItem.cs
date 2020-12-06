using System.ComponentModel.DataAnnotations;
using Force.Ddd;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class AllOrdersItem : HasIdBase
    {
        public override int Id { get; set; }

        public double Total { get; set; }
        
        public string Status { get; set; }
        
        public string Created { get; set; }
        
        [HiddenInput]
        public string UserId { get; set; }
    }
}