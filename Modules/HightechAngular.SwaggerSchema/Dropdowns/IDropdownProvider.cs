using System;

namespace HightechAngular.SwaggerSchema.Dropdowns
{
    public interface IDropdownProvider
    {
        DropdownOptions GetDropdownOptions(Type t);
    }

    public interface IDropdownProvider<T>
    {
        DropdownOptions GetDropdownOptions();
    }
}