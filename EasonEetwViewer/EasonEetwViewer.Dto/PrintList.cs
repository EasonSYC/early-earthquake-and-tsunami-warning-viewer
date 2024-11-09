using System.Text;

namespace EasonEetwViewer.Dto;

public class PrintList<T> : List<T>
{
    public override string ToString()
    {
        return $"List [ {string.Join(", ", this)} ]";
    }
}
