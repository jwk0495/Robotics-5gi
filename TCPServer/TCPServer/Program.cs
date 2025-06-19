using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    class Program
    {
        static void Main()
        {
            // 서버 소켓 생성 + 바인딩(특정 아이피와 포트할당)
            TcpListener server = new TcpListener(IPAddress.Any, 12345);

            server.Start();

            Console.WriteLine("서버 시작");

            while(true)
            {
                // 클라이언트의 연결을 대기
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("클라이언트 연결됨");
            }
        }
    }
}