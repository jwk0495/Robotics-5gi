using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    class Program
    {
        static void Main()
        {
            // 서버에 연결
            TcpClient client = new TcpClient("127.0.0.1", 12345); // 로컬아이피, 포트 

            Console.Write("서버에 연결됨.\n");

            // 1. 네트워크 스트림 얻기
            NetworkStream stream = client.GetStream();

            while(true)
            {
                // 2. 서버에 데이터 보내기
                string data = Console.ReadLine();
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                stream.Write(dataBytes, 0, dataBytes.Length);

                // 3. 서버로 부터 데이터 읽기
                byte[] buffer = new byte[1024];
                int byteNumber = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, byteNumber);
                Console.WriteLine(response);
            }
        }
    }
}