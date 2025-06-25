using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;


// TCP Server(�ܼ����α׷�)�� �԰� ����ϴ� TCPClient
// *���ǻ���* -> TCP Server�� ���� �� �Ŀ� �������ּ���.
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

    const string X_START_UNITY2PLC = "X0";  // Unity�� ��ư ������ PLC�� ������ ���� X����̽� ����Ʈ �ּ�
    const string X_START_PLC2UNITY = "X10"; // PLC�� ���� ������ Unity�� ������ ���� X����̽� ����Ʈ �ּ�
    const string Y_START_PLC2UNITY = "Y0";  // PLC�� ���� ������ Unity�� ������ ���� Y����̽� ����Ʈ �ּ�
    const int X_BLOCKCNT_UNITY2PLC = 1;     // Unity�� ��ư ������ PLC�� ������ X����̽� ��� ����
    const int X_BLOCKCNT_PLC2UNITY = 1;     // PLC�� ���� ������ Unity�� ������ X����̽� ��� ����
    const int Y_BLOCKCNT_PLC2UNITY = 1;     // PLC�� ���� ������ Unity�� ������ Y����̽� ��� ����

    
    [Header("Y ����̽���")] public List<Cylinder> cylinders;
    public Conveyor conveyor;
    public TowerManager towerManager;

    [Header("X ����̽���")] public Sensor ��������;
    public Sensor �ݼӼ���;

    void Start()
    {
        Task.Run(() => InitializeClient());
    }

    // ������ ���տ� Lifecycle �Լ�(Unity�� Main Thread)
    private void Update()
    {
        // 1. Unity -> TCPServer�� ������ ��û
        RequestData(out request);
        // 2. TCPServer -> Unity ����� ����
        ResponseData(ref response);
    }

    // ������� ������ ���� ����
    // Connected, Disconnected, read,x0,1,25,y0,1,25
    private void ResponseData(ref string request)
    {
        if (isConnected)
        {
            string[] splited = response.Split(',');
            
            // 1. ���ڿ� -> ���� �迭
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
            
            // 2. ���� �迭 -> ���� �迭
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
            ��������.isActive              = xData[8] is '1' ? true : false;
            �ݼӼ���.isActive              = xData[9] is '1' ? true : false;
            
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
            // 1. 10���� 336 -> 2���� 101010000
            string binary = Convert.ToString(data[i], 2);

            // 2. ���ư� ������Ʈ �߰� 1/0101/0000 -> 0000/0010/1010/0000
            int upBitCnt = 16 - binary.Length;

            // 3. ������(������Ʈ �ε��� ���� ���) 1/0101/0000 -> 0000/1010/1
            string reversedBinary = new string(binary.Reverse().ToArray());

            // 4. ������Ʈ ���̱� 0000/1010/1 + 000/0000 = 0000/1010/1000/0000
            for(int j = 0; j < upBitCnt; j++)
            {
                reversedBinary += "0";
            }

            result[i] = reversedBinary;
        }

        return result;
    }
    
    // ������ ��û ���� : "Request,read,X0,1,Y0,1,write,X0,1,25"
    private void RequestData(out string request)
    {
        if(isConnected)
        {
            // 1. X ����̽� �б�
            request = $"Request,read,{X_START_PLC2UNITY},{X_BLOCKCNT_UNITY2PLC}";
            
            // 2. Y ����̽� �б�
            request += $"{Y_START_PLC2UNITY},{Y_BLOCKCNT_PLC2UNITY}";
            
            // ����, ����, ������� ������ ù��° ��Ͽ� ����(010 -> ������ 2)
            char power = (isPowerOnCliked == true ? '1' : '0');
            char stop  = (isStopCliked    == true ? '1' : '0');
            char eStop = (isEStopCliked   == true ? '1' : '0');
            string binaryStr = $"{eStop}{stop}{power}"; // "010"
            int decimalX = Convert.ToInt32(binaryStr, 2);
            
            // 3. X ����̽� ����
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

        print("������ ����Ǿ����ϴ�.");

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