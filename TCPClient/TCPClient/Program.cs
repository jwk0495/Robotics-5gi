using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    class Program
    {
        static async Task Main()
        {
            //SyncUpdate();

            // 1. 클라이언스 생성 및 비동기 연결시도
            TcpClient client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", 12345);

            Console.WriteLine("서버에 연결되었습니다.");
            Console.WriteLine("메시지를 입력하고 Enter 키를 눌러주세요.('exit' 입력 시 종료");

            // 2. 서버 통신을 위한 스트림열기
            NetworkStream stream = client.GetStream();

            // 3. 반복 메시지 송수신
            while(true)
            {
                Console.Write("보낼 메시지: ");
                string message = Console.ReadLine();

                if (message.Equals("exit"))
                    break;

                // 4. 메시지를 바이트 배열로 변환하여 전송
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                // 5. 서버로 부터 받은 응답 수신
                byte[] buffer = new byte[1024];
                int byteRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, byteRead);

                Console.WriteLine($"서버 응답: {response}");
            }
        }

        private static void SyncUpdate()
        {
            // 서버에 연결
            TcpClient client = new TcpClient("127.0.0.1", 12345); // 로컬아이피, 포트 

            Console.Write("서버에 연결됨.\n");

            // 1. 네트워크 스트림 얻기
            NetworkStream stream = client.GetStream();

            while (true)
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