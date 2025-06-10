using System;
using UnityEngine;

// Cube를 CylinderA -> CylinderB 로 이동시킨다.
// 속성: 물체의 속도, 시작점, 목적지
public class MovementEx : MonoBehaviour
{
    [SerializeField] private float speed;
    // public float Speed { get; set; }
    public GameObject cylinderA;
    public GameObject cylinderB;
    public bool isCylinderA = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCylinderA)
            MoveAtoB(transform.gameObject, cylinderB);
        else
            MoveAtoB(transform.gameObject, cylinderA);
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
            isCylinderA = !isCylinderA; // false -> true, true -> false
            return;
        }

        // 4. 플레이어에게 단위벡터를 더해줌
        transform.position += normalizedDir * speed * Time.deltaTime; 
    }
}
