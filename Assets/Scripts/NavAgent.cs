using UnityEngine;
using UnityEngine.AI;

// 캡슐이 네비게이션 매시의 특정 위치로 이동한다.
// 속성: target transform
public class NavAgent : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    NavMeshAgent agent;
    Ray ray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = 10;
        agent.angularSpeed = 1000;

        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestination(target2.position);
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭시
        {

            // 스크린 스페이스의 한 지점을 마우스로 찍었을 때
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo = new RaycastHit(); // 레이 발사 후, 충돌 정보 저장

            // 광선을 origin -> direction 발사했을 때, 충돌이 있다면
            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        }
    }
}
