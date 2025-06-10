using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Cube1, 2, 3, 4, 5를 순서대로 1초 간격으로 출발하여
// CylinderA -> B -> C -> D 순으로 이동한다.
// 속성: Cube의 속도, 타겟들

public class CubeManager : MonoBehaviour
{
    public float speed;
    public GameObject cube;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public List<GameObject> targets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CoStart());
    }

    // Update is called once per frame
    void Update()
    {
        print("update");
    }

    // 코루틴 메서드: 프로세스 내에서 잠깐 기다릴 수 있는 기능
    IEnumerator CoStart()
    {
        print("cube 시작"); // 반복문 사용 Cube 운행
        yield return MoveCubeToTargets(cube, targets);

        yield return new WaitForSeconds(1);

        print("cube2 시작"); // Cube1 운행
        yield return MoveCubeToTargets(cube1, targets);

        yield return new WaitForSeconds(1);

        print("cube3 시작"); // Cube2 운행
        yield return MoveCubeToTargets(cube2, targets);

        yield return new WaitForSeconds(1);

        print("cube4 시작"); // Cube3 운행
        yield return MoveCubeToTargets(cube3, targets);

        yield return new WaitForSeconds(1);

        print("cube5 시작"); // Cube4 운행
        yield return MoveCubeToTargets(cube4, targets);

    }

    IEnumerator MoveCubeToTargets(GameObject cube, List<GameObject> targets)
    {
        int index = 0;

        print(cube.gameObject.name + "출발!");

        while(true)
        {
            // 1. A에서 B를 향하는 벡터 -> 단위벡터(크기가 1인 벡터) -> 플레이어에게 단위벡터를 더해줌
            Vector3 direction = targets[index].transform.position - cube.transform.position;
            // 2. 단위벡터(크기가 1인 벡터)
            Vector3 normalizedDir = direction.normalized;

            // 3. 거리계산
            float distance = Vector3.Magnitude(direction);
            // 어디까지 갈 것인가? cylinderB 까지 -> 거리
            print(distance);

            if (distance < 0.1f)
            {
                index++;

                if (index == targets.Count)
                {
                    break;
                }
                // CylA -> CylB -> CylC -> Cyl4
            }

            // 4. 플레이어에게 단위벡터를 더해줌
            cube.transform.position += normalizedDir * speed * Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
