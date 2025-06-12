using System.Collections;
using UnityEngine;

// 실린더 Rod를 minRange만큼 후진, maxRange만큼 전진
// 속성: 실린더 Rod의 transform, minRange Pos, maxRange Pos, 속도
public class Cylinder : MonoBehaviour
{
    public enum SolenoidType
    {
        단방향솔레노이드,
        양방향솔레노이드
    }
    SolenoidType type = SolenoidType.양방향솔레노이드;

    public Transform cylinderRod;
    public float speed; // 공압밸브 조절
    public float minPosY;
    public float maxPosY;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 back = new Vector3(0, minPosY, 0);
            Vector3 front = new Vector3(0, maxPosY, 0);
            StartCoroutine(MoveCylinder(back, front, true));
        }
    }

    IEnumerator MoveCylinder(Vector3 from, Vector3 to, bool isForward)
    {
        if(isForward)
        {
            yield return null;
        }
        else
        {

        }
    }
}
