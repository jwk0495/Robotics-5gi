using UnityEngine;
using ActUtlType64Lib;
using System;
using TMPro;

public class MxComponent : MonoBehaviour
{
    [SerializeField] TMP_Text logTxt;
    ActUtlType64 mxComponent;
    bool isConnected;

    private void Awake()
    {
        mxComponent = new ActUtlType64();

        mxComponent.ActLogicalStationNumber = 1;

        logTxt.text = "Please connect the PLC..";
        logTxt.color = Color.red;
    }

    public void Open()
    {
        int iRet = mxComponent.Open();

        if (iRet == 0)
        {
            isConnected = true;

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
}
