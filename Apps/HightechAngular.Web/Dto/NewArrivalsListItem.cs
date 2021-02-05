using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Index
{
    public class NewArrivalsListItem : HasIdBase
    {
        static NewArrivalsListItem()
        {
            TypeAdapterConfig<Product, SaleListItem>
                .NewConfig()
                .Map(dest => dest.Price, Product.DiscountedPriceExpression)
                .Map(dest => dest.DateCreatedName, src => src.DateCreated.ToString("d"));
        }

        [Display(Name = "Id")]
        public override int Id { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        
        [Display(Name = "Price")]
        public double Price { get; set; }
        
        [Display(Name = "Discount Percent")]
        public int DiscountPercent { get; set; }
        
        [Display(Name = "Date Created")]
        public string DateCreatedName { get; set; }
        
        [HiddenInput]
        public DateTime DateCreated { get; set; }
    }
}