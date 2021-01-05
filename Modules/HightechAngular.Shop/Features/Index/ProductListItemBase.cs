using System;
using System.ComponentModel.DataAnnotations;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Index
{
    public class ProductListItemBase<T>: HasIdBase<int>
        where T: ProductListItemBase<T>
    {
        static ProductListItemBase()
        {
            TypeAdapterConfig<Product, T>
                .NewConfig()
                .Map(dest => dest.Price, Product.DiscountedPriceExpression);
        }

        [Display(Name = "Id")]
        public override int Id { get; set; }

        [Display(Name = "Name")]

        // ReSharper disable UnusedMember.Global
        public string Name { get; set; } = default!;
        
        [Display(Name = "Category")]
        public string CategoryName { get; set; } = default!;

        [Display(Name = "Price")]
        public double Price { get; set; } = default!;

        [Display(Name = "Discount Percent")]
        public int DiscountPercent { get; set; }

        [Display(Name = "Date Created")]
        public string CreatedString => Created.ToString("d");

        [HiddenInput]
        public DateTime Created { get; set; }
        // ReSharper restore UnusedMember.Global
    }
}