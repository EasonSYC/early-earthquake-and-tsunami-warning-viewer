using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket;
public delegate void OnDataReceived(string data, FormatType? format);