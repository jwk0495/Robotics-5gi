using UnityEngine;

public class RayCamera : MonoBehaviour
{
    Ray ray;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭시
        {

            // 스크린 스페이스의 한 지점을 마우스로 찍었을 때
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit(); // 레이 발사 후, 충돌 정보 저장

            // 광선을 origin -> direction 발사했을 때, 충돌이 있다면
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                print(hitInfo.collider.name);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray.origin, ray.direction * 100);
    }
}
