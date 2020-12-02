using Microsoft.AspNetCore.Mvc;

namespace WebApplication.net5.Models
{
    public class DtoWithInitProperties
    {
        [FromQuery]
        public string Value { get; init; }
    }
}