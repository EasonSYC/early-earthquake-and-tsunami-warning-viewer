using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Styles;

namespace EasonEetwViewer.Extensions
{
    internal static class StringColourExtensions
    {
        public static Color ToColour(this string colourString)
            => Color.FromString(colourString);
    }
}
