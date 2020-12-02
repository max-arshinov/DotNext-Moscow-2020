using System;
using System.ComponentModel.DataAnnotations;
using Force.Cqrs;

namespace Infrastructure.Examples.Domain.Features
{
    public class MassIncreasePrice: ICommand<int>
    {
        [Range(1, Int32.MaxValue)]
        public double Price { get; set; }
    }
}