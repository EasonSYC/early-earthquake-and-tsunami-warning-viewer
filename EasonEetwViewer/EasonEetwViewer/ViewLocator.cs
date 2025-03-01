using Avalonia.Controls;
using Avalonia.Controls.Templates;
using EasonEetwViewer.ViewModels.ViewModelBases;

namespace EasonEetwViewer;
/// <summary>
/// The view locator for the application.
/// </summary>
public class ViewLocator : IDataTemplate
{
    /// <inheritdoc/>
    public Control? Build(object? param)
    {
        if (param is null)
        {
            return null;
        }

        string name = param.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        Type? type = Type.GetType(name);

        return type is not null
            ? (Control)Activator.CreateInstance(type)!
            : new TextBlock { Text = "Not Found: " + name };
    }
    /// <inheritdoc/>
    public bool Match(object? data)
        => data is ViewModelBase;
}
