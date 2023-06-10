using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);

using Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

socketServer.Bind(ipEndPoint);
Console.WriteLine(socketServer.LocalEndPoint);
socketServer.Listen();
Console.WriteLine("Server listen...");

while(true)
{
    using Socket socketClient = await socketServer.AcceptAsync();
    Console.WriteLine($"Client address: {socketClient.RemoteEndPoint}");

    string message = "Hello client! " + DateTime.Now.ToLongTimeString();
    byte[] buffer = Encoding.UTF8.GetBytes(message);
    await socketClient.SendAsync(buffer);

    Console.WriteLine($"Message to client: {message}");



    byte[] sizeMessage = new byte[4];
    await socketClient.ReceiveAsync(sizeMessage);

    byte[] bufferMessage = new byte[BitConverter.ToInt32(sizeMessage)];
    await socketClient.ReceiveAsync(bufferMessage);

    Console.WriteLine($"Message from client: {Encoding.UTF8.GetString(bufferMessage)}");


    /*
    StringBuilder response = new StringBuilder();
    var bufferByte = new byte[1];
    List<byte> bufferList = new List<byte>();

    while(true)
    {
        int count = await socketClient.ReceiveAsync(bufferByte);
        if (count == 0 || bufferByte[0] == '\n') break;
        bufferList.Add(bufferByte[0]);
    }

    Console.WriteLine($"Message from client (with marker): {Encoding.UTF8.GetString(bufferList.ToArray())}");
    */

    //int bytes;
    //do
    //{
    //    bytes = await socketClient.ReceiveAsync(buffer);
    //    response.Append(Encoding.UTF8.GetString(buffer));
        
    //} while (bytes > 0);
    //Console.WriteLine($"Response from client: {response}");
}



