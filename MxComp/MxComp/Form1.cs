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

            if(!isOk)
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
    }
}
