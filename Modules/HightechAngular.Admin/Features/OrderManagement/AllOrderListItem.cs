using System;
using System.ComponentModel.DataAnnotations;
using Force.Ddd;
using HightechAngular.Orders.Base;
using HightechAngular.Orders.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class AllOrdersListItem : HasIdBase, IHasCreatedDateString
    {
        static AllOrdersListItem()
        {
            TypeAdapterConfig<Order, AllOrdersListItem>
                .NewConfig()
                .Map(dest => dest.UserName, src => src.User.Email)
                .Map(dest => dest.DisputeComment, src => src.Status == OrderStatus.Disputed
                    ? src.Complaint
                    : src.AdminComment);
        }
        
        [Display(Name = "Id")]
        public override int Id { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        [Display(Name = "Status")]
        public OrderStatus Status { get; set; }

        [HiddenInput]
        public DateTime Created { get; set; } = default!;

        [Display(Name = "Created")]
        public string CreatedString => Created.ToString("d");

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Display(Name = "Comment")]
        public string DisputeComment { get; set; }
    }
}