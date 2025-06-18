using ActUtlType64Lib;

namespace MxComp
{
    public partial class Form1 : Form
    {
        ActUtlType64 mxComponent;
        bool isConnected;

        public Form1()
        {
            InitializeComponent();

            label1.Text = "프로그램을 시작합니다.";

            mxComponent = new ActUtlType64();

            mxComponent.ActLogicalStationNumber = 1;
        }

        private void Exit(object sender, FormClosingEventArgs e)
        {
            Close(sender, e);
        }

        private void Open(object sender, EventArgs e)
        {
            int iRet = mxComponent.Open();

            if (iRet == 0)
            {
                isConnected = true;

                label1.Text = "잘 연결이 되었습니다.";
            }
            else
            {
                // 에러코드 반환(16진수)
                label1.Text = Convert.ToString(iRet, 16);
            }
        }

        private void Close(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                label1.Text = "이미 연결해지 상태입니다.";

                return;
            }

            int iRet = mxComponent.Close();

            if (iRet == 0)
            {
                isConnected = false;

                label1.Text = "잘 해지가 되었습니다.";
            }
            else
            {
                // 에러코드 반환(16진수)
                label1.Text = Convert.ToString(iRet, 16);
            }
        }

        private void GetDevice(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                label1.Text = "디바이스 이름을 입력해 주세요.";
                return;
            }

            int data = 0;
            int iRet = mxComponent.GetDevice(textBox1.Text, out data);

            if (iRet == 0)
            {
                label1.Text = $"{textBox1.Text}: {data}";
            }
            else
            {
                // 에러코드 반환(16진수)
                label1.Text = Convert.ToString(iRet, 16);
            }
        }

        private void SetDevice(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                label1.Text = "디바이스 이름 또는 값을 입력해 주세요.";
                return;
            }

            int value = 0;
            bool isOk = int.TryParse(textBox2.Text, out value);

            if (!isOk)
            {
                label1.Text = "숫자를 입력해 주세요.";
                return;
            }

            int iRet = mxComponent.SetDevice(textBox1.Text, value);

            if (iRet == 0)
            {
                label1.Text = $"{textBox1.Text}: {textBox2.Text}가 잘 적용되었습니다.";
            }
            else
            {
                // 에러코드 반환(16진수)
                label1.Text = Convert.ToString(iRet, 16);
            }
        }

        private void ReadDeviceBlock(object sender, EventArgs e)
        {
            int deviceBlockCnt = 0;
            bool isOk = int.TryParse(textBox3.Text, out deviceBlockCnt);

            if (!isOk)
            {
                label1.Text = "블록의 개수를 정수형으로 입력해 주세요.";

                return;
            }

            int[] data = new int[deviceBlockCnt];
            int iRet = mxComponent.ReadDeviceBlock(textBox1.Text, deviceBlockCnt, out data[0]);

            if (iRet == 0)
            {
                // 3개 블록 -> { 555, 125, 0 }
                // n개 블록 -> { x, y, z, i, j, k, }
                // X0부터 한 블록 사용시 result[0] = X0
                string result = "{ ";
                for (int i = 0; i < data.Length; i++)
                {
                    // 1. 10진수 336 -> 2진수 101010000
                    string binary = Convert.ToString(data[i], 2);

                    // 2. 날아간 상위비트를 추가해준다. 1/0101/0000 -> 0000/0001/0101/0000
                    int strLength = binary.Length; // 9
                    int upBitCnt = 16 - strLength; // 16 - 9 = 7

                    // 3. 리버스 1/0101/0000 -> 0000/1010/1
                    string reversedBinary = new string(binary.Reverse().ToArray());

                    // 4. 상위비트 붙이기 0000/1010/1 + 000/0000
                    for (int j = 0; j < upBitCnt; j++)
                    {
                        reversedBinary += "0";
                    }

                    result += reversedBinary + ", "; // 0000/1010/1000/0000
                }
                result += " }";

                label1.Text = result; // { 0000101010000000, 0000101010000000 }
            }
        }

        // 주소: 블록의 첫 포인트 기준 
        private void WriteDeviceBlock(object sender, EventArgs e)
        {
            // 블록 개수: 3, 디바이스 값: "333,55,2"
            int deviceBlockCnt = 0;
            bool isOk = int.TryParse(textBox3.Text, out deviceBlockCnt);

            if (!isOk)
            {
                label1.Text = "블록의 개수를 정수형으로 입력해 주세요.";
                return;
            }

            // 디바이스 값: "333,55,2" -> { 333, 55, 2 }
            string[] values = textBox2.Text.Split(",");
            int[] numbers = Array.ConvertAll(values, int.Parse); // Try.Parse로 변경예정

            int iRet = mxComponent.WriteDeviceBlock(textBox1.Text, deviceBlockCnt, ref numbers[0]);

            if (iRet == 0)
            {
                label1.Text = "쓰기가 완료되었습니다.";
            }
            else
            {
                label1.Text = Convert.ToString(iRet, 16);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
