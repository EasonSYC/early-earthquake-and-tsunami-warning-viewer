using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum.WebSocket;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;
public delegate void OnDataReceived(string data, FormatType? format);