using System;
using System.ComponentModel.DataAnnotations;
using Force.Ddd;
using HightechAngular.Orders.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop.Features.Catalog
{
    public class ProductListItem : HasIdBase
    {
        static ProductListItem()
        {
            TypeAdapterConfig<Product, ProductListItem>
                .NewConfig()
                .Map(dest => dest.CreatedString, src => src.Created.ToString("d"));
        }

        [Display(Name = "Id")]
        public override int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = default!;

        [Display(Name = "Category")]
        public string CategoryName { get; set; } = default!;

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Discount Percent")]
        public int DiscountPercent { get; set; }

        [Display(Name = "Created")]
        public string CreatedString { get; set; } = default!;

        [HiddenInput]
        public DateTime Created { get; set; }

        [HiddenInput]
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return DiscountPercent > 0
                ? $"{Name} ${Price} Sale: ${DiscountPercent}%!"
                : $"{Name} ${Price}";
        }
    }
}