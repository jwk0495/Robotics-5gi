using System.Net.Sockets;

namespace TCPClient
{
    class Program
    {
        static void Main()
        {
            // 서버에 연결
            TcpClient client = new TcpClient("127.0.0.1", 12345); // 로컬아이피, 포트 

            Console.Write("서버에 연결됨.");
        }
    }
}