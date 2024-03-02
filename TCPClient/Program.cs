using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        try{
        //Connect to the server on the specified IPAddress and port
        TcpClient client = new TcpClient("127.0.0.1",8888);

        //Get the stream object associated with the client
        NetworkStream stream = client.GetStream();

        //Send a message to the server
        string message = "Hello from the client!";
        byte[] data = Encoding.ASCII.GetBytes(message);
        stream.Write(data,0,data.Length);
        Console.WriteLine($"Sent: {message}");

        //Recieve the response from server
        data = new byte[256];
        int bytes = stream.Read(data,0,data.Length);
        string response = Encoding.ASCII.GetString(data,0,bytes);
        Console.WriteLine($"Recieved: {response}");

        //Close the connection
        client.Close();
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
}