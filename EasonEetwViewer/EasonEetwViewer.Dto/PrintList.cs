namespace EasonEetwViewer.Dto;

/// <summary>
/// A wrapping around <c>List&lt;T&gt;</c> to provide <c>ToString</c> method similar to that of a record.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class PrintList<T> : List<T>
{
    /// <summary>
    /// Returns a string representing the list.
    /// </summary>
    /// <returns>A string representing the list.</returns>
    public override string ToString() => $"List [ {string.Join(", ", this)} ]";
}
