using System.Reflection;
using Avalonia.Data.Converters;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Converters;

internal class EnumDisplayColourConverter : FuncValueConverter<Enum?, string>
{
    public EnumDisplayColourConverter() : base(value
        => (string)typeof(EnumDisplayColourExtensions)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(method
            => method.Name is nameof(EnumDisplayColourExtensions.ToColourString))
        .Where(method
            => method.GetParameters().Length is 1)
        .SingleOrDefault(method
            => method.GetParameters()[0].ParameterType == value?.GetType())?
        .Invoke(null, [value])!)
    { }
}