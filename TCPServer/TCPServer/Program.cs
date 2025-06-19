using System.Net;
using System.Net.Sockets;
using System.Text;

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

            // 1. 클라이언트의 연결을 대기
            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("클라이언트 연결됨.\n");

            // 2. 네트워크의 스트림 가져오기
            NetworkStream stream = client.GetStream();

            while(true)
            {
                // 3. 클라이언트로 부터 데이터 읽기
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                // 4. 버퍼에 저장한 데이터를 인코딩(UTF8)
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("클라이언트: " + data);

                // 5. 답장 쓰기(데이터 전송하기)
                string response = "서버: 데이터 받음\n";
                byte[] responseData = Encoding.UTF8.GetBytes(response);
                stream.Write(responseData, 0, responseData.Length);
            }
        }
    }
}