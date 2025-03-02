using System.Reflection;
using Avalonia.Data.Converters;
using EasonEetwViewer.Extensions;

namespace EasonEetwViewer.Converters;

internal class EnumDisplayTextConverter : FuncValueConverter<Enum?, string>
{
    public EnumDisplayTextConverter() : base(value
        => (string)typeof(EnumDisplayTextExtensions)
        .GetMethods(BindingFlags.Static | BindingFlags.Public)
        .Where(method
            => method.Name is nameof(EnumDisplayTextExtensions.ToDisplayString))
        .Where(method
            => method.GetParameters().Length is 1)
        .SingleOrDefault(method
            => method.GetParameters()[0].ParameterType == value?.GetType())?
        .Invoke(null, [value])!)
    { }
}