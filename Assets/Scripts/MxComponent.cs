using UnityEngine;
using ActUtlType64Lib;
using System;

public class MxComponent : MonoBehaviour
{
    ActUtlType64 mxComponent;
    bool isConnected;

    private void Awake()
    {
        mxComponent = new ActUtlType64();

        mxComponent.ActLogicalStationNumber = 1;
    }

    public void Open()
    {
        int iRet = mxComponent.Open();

        if (iRet == 0)
        {
            isConnected = true;

            print("잘 연결이 되었습니다.");
        }
        else
        {
            // 에러코드 반환(16진수)
            print(Convert.ToString(iRet, 16));
        }
    }

    public void Close()
    {
        if (!isConnected)
        {
            print("이미 연결해지 상태입니다.");

            return;
        }

        int iRet = mxComponent.Close();

        if (iRet == 0)
        {
            isConnected = false;

            print("잘 해지가 되었습니다.");
        }
        else
        {
            // 에러코드 반환(16진수)
            print(Convert.ToString(iRet, 16));
        }
    }
}
