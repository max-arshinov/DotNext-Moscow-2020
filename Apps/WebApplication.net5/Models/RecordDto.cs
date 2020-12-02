using Microsoft.AspNetCore.Mvc;

namespace WebApplication.net5.Models
{
    public record RecordDto
    {
        [FromQuery]
        public string FirstName { get; init; }
    
        [FromQuery]    
        public string LastName { get; init; }
    }
}