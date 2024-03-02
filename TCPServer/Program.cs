using System.Net;
using System.Net.Sockets;
using System.Text;
internal class Program
{
    private static void Main(string[] args)
    {
        TcpListener server = null;
        try{
            //Set the TcpListener on a specific port (e.g 8888)
            int port = 8888;
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");

            //TcpListener listens to incoming connections
            server = new TcpListener(localAddress,port);

            //Start listening for client's requests
            server.Start();

            Console.WriteLine("server is listening...");

            //Enter listening loop
            while(true){
            //Perform a blocking call to accept request
            //You could also use server.AcceptSocket() here
            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("Client connected!");

            //Set the stream object associated with the client
            NetworkStream stream = client.GetStream();

            //Read the incoming data
            byte[] data = new byte[256];
            int bytes = stream.Read(data,0,data.Length);
            string message = Encoding.ASCII.GetString(data,0,bytes);
            Console.WriteLine($"Recieved: {message}");

            //Send a response back to the client
            byte[] response = Encoding.ASCII.GetBytes("Hello from the server");
            stream.Write(response,0,response.Length);

            //Close the connection
            client.Close();
            }
        }catch(Exception e){
            Console.WriteLine(e.Message);
        }
    }
}