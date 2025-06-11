using System.Collections;
using UnityEngine;

// RGB Lamp의 색상을 정한 시간에 따라 순차적으로 깜빡이게 한다.
// 속성: 시간, Lamp의 색상
public class LampManager : MonoBehaviour
{
    public float time;
    public Renderer redLamp;
    public Renderer greenLamp;
    public Renderer blueLamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 처음시작시 색상을 검게 초기화
        // _Color는 Shader의 특정 속성: 파이프라인에 따라 다름
        redLamp.material.SetColor("_BaseColor", Color.black);
        greenLamp.material.SetColor("_BaseColor", Color.black);
        blueLamp.material.SetColor("_BaseColor", Color.black);

        StartCoroutine(CoStartLamp());
    }

    // 코루틴을 사용, time 간격으로 점등 점멸하는 Lamp
    IEnumerator CoStartLamp()
    {
        redLamp.material.SetColor("_BaseColor", Color.red);
        greenLamp.material.SetColor("_BaseColor", Color.black);
        blueLamp.material.SetColor("_BaseColor", Color.black);

        yield return new WaitForSeconds(1);

        redLamp.material.SetColor("_BaseColor", Color.black);
        greenLamp.material.SetColor("_BaseColor", Color.green);
        blueLamp.material.SetColor("_BaseColor", Color.black);

        yield return new WaitForSeconds(1);

        redLamp.material.SetColor("_BaseColor", Color.black);
        greenLamp.material.SetColor("_BaseColor", Color.black);
        blueLamp.material.SetColor("_BaseColor", Color.blue);
    }
}
