using System.Collections;
using UnityEngine;

// Lerp 메서드를 사용해서 Sphere를 3초 동안 A에서 B 지점으로 이동시킨다
// 속성: 시간, A지점, B지점
public class LerpMovement : MonoBehaviour
{
    [Range(0, 1f)] public float ratio;
    [SerializeField] float time;
    public float elapsedTime;
    public Transform pointA;
    public Transform pointB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CoMoveAtoB(pointA.position, pointB.position, time));
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 지나간 시간을 계산
        //elapsedTime += Time.deltaTime;

        //if (elapsedTime > time)
        //    elapsedTime = 0;

        //Vector3 position = Vector3.Lerp(pointA.position, pointB.position, elapsedTime / time);
        //transform.position = position;        
    }

    // A에서 B까지 나 자신을 이동
    IEnumerator CoMoveAtoB(Vector3 a, Vector3 b, float t)
    {
        while(true)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > time)
            {
                elapsedTime = 0;
                break;
            }

            Vector3 position = Vector3.Lerp(a, b, elapsedTime / t);
            transform.position = position;

            yield return new WaitForEndOfFrame();
        }
    }
}
