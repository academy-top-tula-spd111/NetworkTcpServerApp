using System.Net;
using System.Net.Sockets;
using System.Text;

using Socket socketClient = new Socket(AddressFamily.InterNetwork, 
                                       SocketType.Stream, 
                                       ProtocolType.Tcp);

try
{
    await socketClient.ConnectAsync(IPAddress.Loopback, 8888);
    Console.WriteLine($"\tConnect with server: {socketClient.RemoteEndPoint}");
    byte[] buffer = new byte[1024];
    int bites = await socketClient.ReceiveAsync(buffer);
    string message = Encoding.UTF8.GetString(buffer);

    Console.WriteLine($"Message from server: {message}");

    Thread.Sleep(1000);
    message = "Hello server! " + DateTime.Now.ToLongTimeString() + "\nAppend text";
    buffer = Encoding.UTF8.GetBytes(message);
    
    Console.WriteLine();
    Console.WriteLine($"Message to server: {message}");
    await socketClient.SendAsync(BitConverter.GetBytes(buffer.Length));
    await socketClient.SendAsync(buffer);
}
catch (SocketException e)
{
    Console.WriteLine($"not connect: {e.Message}");
}