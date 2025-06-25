using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


// TCP Server(콘솔프로그램)와 함계 사용하는 TCPClient
// *주의사항* -> TCP Server를 먼저 켠 후에 실행해주세요.
public class TCPClient : MonoBehaviour
{
    TcpClient client;
    NetworkStream stream;
    public string request; // "Connect", "Disconnect", "Request,read,X0,1", "Request,wrtite,X0,1"
    public string response; // "read,X10,1,36", "connected", "Disconnected", "Fale"
    byte[] buffer = new byte[1024];

    [SerializeField] TMP_Text logTxt;
    bool isConnected;
    bool isPowerOnCliked;
    bool isStopCliked;
    bool isEStopCliked;

    const string X_START_UNITY2PLC = "X0";  // Unity의 버튼 정보를 PLC로 보내는 시작 X디바이스 포인트 주소
    const string X_START_PLC2UNITY = "X10"; // PLC의 센서 정보를 Unity로 보내는 시작 X디바이스 포인트 주소
    const string Y_START_PLC2UNITY = "Y0";  // PLC의 센서 정보를 Unity로 보내는 시작 Y디바이스 포인트 주소
    const int X_BLOCKCNT_UNITY2PLC = 1;     // Unity의 버튼 정보를 PLC로 보내는 X디바이스 블록 개수
    const int X_BLOCKCNT_PLC2UNITY = 1;     // PLC의 센서 정보를 Unity로 보내는 X디바이스 블록 개수
    const int Y_BLOCKCNT_PLC2UNITY = 1;     // PLC의 설비 정보를 Unity로 보내는 Y디바이스 블록 개수

    
    [Header("Y 디바이스용")] public List<Cylinder> cylinders;
    public Conveyor conveyor;
    public TowerManager towerManager;

    [Header("X 디바이스용")] public Sensor 근접센서;
    public Sensor 금속센서;

    void Start()
    {
        Task.Run(() => InitializeClient());
    }

    // 데이터 취합용 Lifecycle 함수(Unity의 Main Thread)
    private void Update()
    {
        // 1. Unity -> TCPServer로 데이터 요청
        RequestData(out request);
        // 2. TCPServer -> Unity 설비로 적용
        ResponseData(ref response);
    }

    // 응답받은 정보를 설비에 적용
    // Connected, Disconnected, read,x0,1,25,y0,1,25
    private void ResponseData(ref string request)
    {
        if (isConnected)
        {
            string[] splited = response.Split(',');
            
            // 1. 문자열 -> 숫자 배열
            string xData = splited[3];  // 25 -> 10010
            string yData = splited[6];  // 25 -> 10010

            /* --------------------| Parsing |-------------------- */
            int xInt = 0;
            bool isXint = int.TryParse(xData, out xInt );
            if(!isXint) return;

            int yInt = 0;
            bool isyint = int.TryParse(yData, out yInt );
            if(!isyint) return;
            /* --------------------| Parsing |-------------------- */

            int[] result = { xInt, yInt };
            
            // 2. 숫자 배열 -> 문자 배열
            string[] binaries = ConvertDecimalToBinary(result);
            
            xData = binaries[0];
            yData = binaries[1];
            
            
            /* ------------------| X Device PLC -> Unity |------------------ */
            cylinders[0].isFrontLimitSWON = xData[0] is '1' ? true : false;
            cylinders[0].isBackLimitSWON  = xData[1] is '1' ? true : false;
            cylinders[1].isFrontLimitSWON = xData[2] is '1' ? true : false;
            cylinders[1].isBackLimitSWON  = xData[3] is '1' ? true : false;
            cylinders[2].isFrontLimitSWON = xData[4] is '1' ? true : false;
            cylinders[2].isBackLimitSWON  = xData[5] is '1' ? true : false;
            cylinders[3].isFrontLimitSWON = xData[6] is '1' ? true : false;
            cylinders[3].isBackLimitSWON  = xData[7] is '1' ? true : false;
            근접센서.isActive              = xData[8] is '1' ? true : false;
            금속센서.isActive              = xData[9] is '1' ? true : false;
            
            /* ------------------| Y Device PLC -> Unity |------------------ */
            cylinders[0].isForward      = yData[0]  is '1' ? true : false;
            cylinders[0].isBackward     = yData[1]  is '1' ? true : false;
            cylinders[1].isForward      = yData[2]  is '1' ? true : false;
            cylinders[1].isBackward     = yData[3]  is '1' ? true : false;
            cylinders[2].isForward      = yData[4]  is '1' ? true : false;
            cylinders[2].isBackward     = yData[5]  is '1' ? true : false;
            cylinders[3].isForward      = yData[6]  is '1' ? true : false;
            cylinders[3].isBackward     = yData[7]  is '1' ? true : false;
            conveyor.isCW               = yData[8]  is '1' ? true : false;
            conveyor.isCCW              = yData[9]  is '1' ? true : false;
            towerManager.isRedLampOn    = yData[10] is '1' ? true : false;
            towerManager.isYellowLampOn = yData[11] is '1' ? true : false;
            towerManager.isGreenLampOn  = yData[12] is '1' ? true : false;
        }
    }
    
    // { 336, 55 } -> { 0001110011100000, 0001110011100000 }
    private string[] ConvertDecimalToBinary(int[] data)
    {
        string[] result = new string[data.Length];

        for(int i = 0; i < data.Length; i++)
        {
            // 1. 10진수 336 -> 2진수 101010000
            string binary = Convert.ToString(data[i], 2);

            // 2. 날아간 상위비트 추가 1/0101/0000 -> 0000/0010/1010/0000
            int upBitCnt = 16 - binary.Length;

            // 3. 리버스(하위비트 인덱스 부터 사용) 1/0101/0000 -> 0000/1010/1
            string reversedBinary = new string(binary.Reverse().ToArray());

            // 4. 상위비트 붙이기 0000/1010/1 + 000/0000 = 0000/1010/1000/0000
            for(int j = 0; j < upBitCnt; j++)
            {
                reversedBinary += "0";
            }

            result[i] = reversedBinary;
        }

        return result;
    }
    
    // 데이터 요청 형식 : "Request,read,X0,1,Y0,1,write,X0,1,25"
    private void RequestData(out string request)
    {
        if(isConnected)
        {
            // 1. X 디바이스 읽기
            request = $"Request,read,{X_START_PLC2UNITY},{X_BLOCKCNT_UNITY2PLC}";
            
            // 2. Y 디바이스 읽기
            request += $"{Y_START_PLC2UNITY},{Y_BLOCKCNT_PLC2UNITY}";
            
            // 전원, 정지, 긴급정지 정보를 첫번째 블록에 전달(010 -> 정수형 2)
            char power = (isPowerOnCliked == true ? '1' : '0');
            char stop  = (isStopCliked    == true ? '1' : '0');
            char eStop = (isEStopCliked   == true ? '1' : '0');
            string binaryStr = $"{eStop}{stop}{power}"; // "010"
            int decimalX = Convert.ToInt32(binaryStr, 2);
            
            // 3. X 디바이스 쓰기
            request += $"{X_START_PLC2UNITY},{X_BLOCKCNT_PLC2UNITY},{decimalX}";
        }

        else
        {
            request = "";
        }
    }

    async Task InitializeClient()
    {
        client = new TcpClient();

        await client.ConnectAsync("127.0.0.1", 12345);

        print("서버에 연결되었습니다.");

        stream = client.GetStream();

        while (true)
        {
            if (request.Length != 0)
            {
                byte[] data = Encoding.UTF8.GetBytes(request);

                await stream.WriteAsync(data, 0, data.Length);

                int byteRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, byteRead);
                
                print(response);
                
                if(response.Contains("Connected"))  isConnected = true;
                
                else if (response.Contains("Disconnected")) isConnected = false;
            }
        }
    }

    public void OnConnectBtnClkEvent()
    {
        request = "Connect";
    }

    public void OnDisconnectBtnClkEvent()
    {
        request = "Disconnect";
        
        isConnected = false;
    }
}