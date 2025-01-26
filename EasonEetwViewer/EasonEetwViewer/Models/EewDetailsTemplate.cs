using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace EasonEetwViewer.Models;
internal class EewDetailsTemplate : ObservableObject
{
    internal DateTimeOffset ExpiryTime { get; }
    internal string EventId { get; }
}
