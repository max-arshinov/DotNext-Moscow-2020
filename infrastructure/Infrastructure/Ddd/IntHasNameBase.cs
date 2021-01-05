using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Ddd
{
    public class IntHasNameBase : IntHasIdBase
    {
        [Required]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}