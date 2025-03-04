using System.Reflection;
using Avalonia.Data.Converters;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Converters;

internal class UnitValueDisplayUnitConverter : FuncValueConverter<object, string>
{
    public UnitValueDisplayUnitConverter() : base(value
        => (string)typeof(UnitValueExtensions)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(method
            => method.Name is nameof(UnitValueExtensions.ToUnitString))
        .Where(method
            => method.GetParameters().Length is 1)
        .SingleOrDefault(method
            => method.GetParameters()[0].ParameterType == value?.GetType())?
        .Invoke(null, [value])!)
    { }
}