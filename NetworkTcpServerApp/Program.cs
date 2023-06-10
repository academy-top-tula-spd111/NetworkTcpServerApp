using System.Net;
using System.Net.Sockets;

IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);

using Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socketServer.Bind(ipEndPoint);
Console.WriteLine(socketServer.LocalEndPoint);
socketServer.Listen(1000);
Console.WriteLine("Server listen...");

using Socket socketClient = await socketServer.AcceptAsync();

Console.WriteLine($"Client address: {socketClient.RemoteEndPoint}");