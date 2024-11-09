using System.Text;

namespace EasonEetwViewer.Dto;

/// <summary>
/// A wrapping around <c>List&lt;T&gt;</c> to provide <c>ToString</c> method similar to that of a record.
/// Inherits from <c>List&lt;T&gt;</c>.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class PrintList<T> : List<T>
{
    public override string ToString()
    {
        return $"List [ {string.Join(", ", this)} ]";
    }
}
