using System.Net.Sockets;

using Socket socketClient = new Socket(AddressFamily.InterNetwork, 
                                       SocketType.Stream, 
                                       ProtocolType.Tcp);

try
{
    await socketClient.ConnectAsync("127.0.0.1", 8888);
    Console.WriteLine($"\tConnect with server: {socketClient.RemoteEndPoint}");
}
catch (SocketException e)
{
    Console.WriteLine($"not connect: {e.Message}");
}