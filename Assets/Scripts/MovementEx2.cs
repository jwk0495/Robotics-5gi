using System.Reflection;
using UnityEngine;

// Cube를 CylA -> CylB -> CylC -> CylD 로 순차적으로 이동 
// 속성: 물체의 속도, 실린더 배열
public class MovementEx2 : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject[] targets;
    private int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // targets[0] -> targets[1] -> targets[2] -> targets[3] 
        MoveAtoB(transform.gameObject, targets[index]);
    }

    private void MoveAtoB(GameObject start, GameObject end)
    {
        // 1. A에서 B를 향하는 벡터 -> 단위벡터(크기가 1인 벡터) -> 플레이어에게 단위벡터를 더해줌
        Vector3 direction = end.transform.position - start.transform.position;
        // 2. 단위벡터(크기가 1인 벡터)
        Vector3 normalizedDir = direction.normalized;

        // 3. 거리계산
        float distance = Vector3.Magnitude(direction);
        // 어디까지 갈 것인가? cylinderB 까지 -> 거리
        // print(distance);

        if (distance < 0.1f)
        {
            index++;

            if (index == targets.Length)
            {
                index = 0;
                return;
            }

            return;
        }

        // 4. 플레이어에게 단위벡터를 더해줌
        transform.position += normalizedDir * speed * Time.deltaTime;
    }
}
