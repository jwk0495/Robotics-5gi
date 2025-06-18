using UnityEngine;
using ActUtlType64Lib;
using System;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MxComponent : MonoBehaviour
{
    [SerializeField] TMP_Text logTxt;
    ActUtlType64 mxComponent;
    bool isConnected;
    const string inputStartDevice = "X0";
    const int inputBlockCnt = 2;
    const string outputStartDevice = "Y0";
    const int outBlockCnt = 1;

    // Y디바이스를 받는 설비들 참조
    public List<Cylinder> cylinders;
    public Conveyor conveyor;
    public TowerManager towerManager;
    WaitForSeconds updateInterval = new WaitForSeconds(0.5f);


    private void Awake()
    {
        mxComponent = new ActUtlType64();

        mxComponent.ActLogicalStationNumber = 1;

        logTxt.text = "Please connect the PLC..";
        logTxt.color = Color.red;
    }

    // 특정 시간에 한번씩 반복하여 PLC 데이터를 읽어온다.
    IEnumerator UpdatePLCData()
    {
        while(isConnected)
        {
            ReadDeviceBlock(outputStartDevice, outBlockCnt);

            yield return updateInterval;
        }
    }

    private void OnDestroy()
    {
        if(isConnected)
            Close();   
    }

    public void Open()
    {
        int iRet = mxComponent.Open();

        if (iRet == 0)
        {
            isConnected = true;

            StartCoroutine(UpdatePLCData()); // 데이터 계속 불러오기

            logTxt.text = "PLC is connected!";
            logTxt.color = Color.green;
            print("잘 연결이 되었습니다.");
        }
        else
        {
            // 에러코드 반환(16진수)
            ShowError(iRet);
            print(Convert.ToString(iRet, 16));
        }
    }

    private void ShowError(int iRet)
    {
        logTxt.text = "Error: " + Convert.ToString(iRet, 16);
        logTxt.color = Color.red;
    }

    public void Close()
    {
        if (!isConnected)
        {
            logTxt.text = "PLC is already disconnected.";
            logTxt.color = Color.red;
            print("이미 연결해지 상태입니다.");

            return;
        }

        int iRet = mxComponent.Close();

        if (iRet == 0)
        {
            isConnected = false;

            logTxt.text = "PLC is disconnected completely.";
            logTxt.color = Color.red;
            print("잘 해지가 되었습니다.");
        }
        else
        {
            // 에러코드 반환(16진수)
            ShowError(iRet);
            print(Convert.ToString(iRet, 16));
        }
    }

    /*
    X디바이스 개수(13개) -> X0 부터 2개 블록 사용
    전원버튼 1개(X0)
    정지버튼 1개
    긴급정지버튼 1개
    공급 LS 2개(X10)
    가공 LS 2개
    송출 LS 2개
    배출 LS 2개
    근접센서 1개
    금속센서 1개

    Y디바이스 개수(13개) -> Y0 부터 1개 블록 사용
    공급 Syl 전진/후진 2개
    가공 Syl 전진/후진 2개
    송출 Syl 전진/후진 2개
    배출 Syl 전진/후진 2개
    컨베이어 CW/CCW 2개
    램프 3개
    */

    public void ReadDeviceBlock(string startDevice, int blockCnt)
    {
        // { 336, 55 } -> 0001/1100/1110/0000
        int[] data = new int[blockCnt];
        int iRet = mxComponent.ReadDeviceBlock(startDevice, blockCnt, out data[0]);

        if(iRet == 0)
        {
            // { 0001110011100000, 0001110011100000 }
            string[] result = ConvertDecimalToBinary(data); // 336 -> 0001/1100/1110/0000


            // 씬의 설비에 data를 적용
            // cylinders[0].isForward = data[0]
            // 1. Input X Device 전용 명령


            // 2. output Y Device 전용 명령: 1개 블록만 사용
            if(startDevice.Contains("Y"))
            {
                string y = result[0]; // 001110011100000
                bool isActive = y[0] is '1' ? true : false;
                cylinders[0].isForward      = isActive;
                cylinders[0].isBackward     = y[1] is '1' ? true : false;
                cylinders[1].isForward      = y[2] is '1' ? true : false;
                cylinders[1].isBackward     = y[3] is '1' ? true : false;
                cylinders[2].isForward      = y[4] is '1' ? true : false;
                cylinders[2].isBackward     = y[5] is '1' ? true : false;
                cylinders[3].isForward      = y[6] is '1' ? true : false;
                cylinders[3].isBackward     = y[7] is '1' ? true : false;
                conveyor.isCW               = y[8] is '1' ? true : false;
                conveyor.isCCW              = y[9] is '1' ? true : false;
                towerManager.isRedLampOn    = y[10] is '1' ? true : false;
                towerManager.isYellowLampOn = y[11] is '1' ? true : false;
                towerManager.isGreenLampOn  = y[12] is '1' ? true : false;

                print(y);
            }

        }
        else
        {
            ShowError(iRet);
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
}
