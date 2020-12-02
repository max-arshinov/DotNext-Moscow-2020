using System.Threading.Tasks;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal class BoolDropdownBuilder : DropdownBuilder
    {
        private readonly string _memberName;
        private readonly (string, string) _naming;

        public BoolDropdownBuilder(string memberName, (string trueName, string falseName)? naming)
        {
            _memberName = memberName;
            _naming = naming ?? (true.ToString(), false.ToString());
        }

        public override Task<Dropdown> BuildAsync()
        {
            var options = new[]
            {
                DropdownOption.Create(_naming.Item1, true),
                DropdownOption.Create(_naming.Item2, false)
            };

            return Task.FromResult(new Dropdown(options, _memberName));
        }
    }
}