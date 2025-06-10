using System;
using UnityEngine;
using UnityEngine.UIElements;

// Unity의 Lifecycle 메서드
// 물체를 방향키의 입력을 받아 이동시킨다.
public class MovePlayer : MonoBehaviour
{
    public float speed = 2;
    public float rotSpeed = 2;
    float xRot;
    float yRot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("살아남");

        // 회전 초기화
        transform.rotation = Quaternion.identity; // 회전이 없는 상태
        //transform.rotation = Quaternion.Euler(30, 45, 60);
    }

    // 100FPS: 1초에 100번 업데이트 함수가 작동
    // 30FPS ~ 400FPS
    // Update is called once per frame
    void Update()
    {
        // MoveWithoutTime();

        MoveWithTime();

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        // 오일러회전: 0~360 이해하기 쉬운 각도의 값을 넣어서 회전(직관적)
        // 쿼터니언회전: 4원수(x,y,z,w), 오일러회전의 단점인 짐벌락(gimbal lock)을 보완
        // 짐벌락: 내부의 회전이 외부에 회전에 의해 자유도를 잃어버리는 현상

        // transform.eulerAngles = new Vector3(30, 45, 60);

        // 오일러 회전 짐벌락의 예시
        // transform.eulerAngles += new Vector3(1 * 0.1f, 1 * 0.1f, 0);

        // 쿼터니언 회전 예시
        // transform.Rotate(transform.up, 0.1f); // 내 자신의 Up 벡터 기준 회전
        // transform.Rotate(transform.right, 0.1f); // 내 자신의 Right 벡터 기준 회전

        // transform.rotation = Quaternion.identity; // 회전이 없는 상태

        // Rotate와 같은 기능
        // Quaternion rotY90 = Quaternion.AngleAxis(0.1f, Vector3.up); // 쿼터니언 정의
        // transform.rotation *= rotY90; // Quaternion은 곱한다.(Vector3와의 차이)

        //Vector3 rotDir = Input.mousePosition;
        //print(rotDir);

        float mouseX = Input.GetAxis("Mouse X"); // 수평값 -1 ~ 1
        float mouseY = Input.GetAxis("Mouse Y"); // 수직값 -1 ~ 1

        print($"mouseX: {mouseX}, mouseY: {mouseY}");

        xRot += mouseX * rotSpeed * Time.deltaTime;
        yRot += mouseY * rotSpeed * Time.deltaTime;

        // -90 ~ 90
        yRot = Mathf.Clamp(yRot, -90, 90);

        transform.rotation = Quaternion.Euler(-yRot, xRot, 0);
    }

    private void MoveWithTime()
    {
        // 조이스틱의 인풋을 모방한 -1 ~ 1을 표현하는 함수
        float horizontalInput = Input.GetAxis("Horizontal"); // 방향키 좌우
        float verticalInput = Input.GetAxis("Vertical");     // 방향키 상하

        // 일반적인 벡터 정의방법
        //Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

        // 오브젝트 기준의 벡터 정의 방법
        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
        transform.position += direction * speed * Time.deltaTime; // 0.03s 
    }


    // 시간에 대한 고려 없는 Move메서드
    private void MoveWithoutTime()
    {
        bool isWKeyDown = Input.GetKey(KeyCode.W);
        bool isAKeyDown = Input.GetKey(KeyCode.A);
        bool isSKeyDown = Input.GetKey(KeyCode.S);
        bool isDKeyDown = Input.GetKey(KeyCode.D);

        if (isWKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.forward * speed;        // 월드 좌표계 기준
            Vector3 localDirection = transform.forward * speed; // 로컬 좌표계 기준

            // 월드좌표의 내 현재위치 + 방향벡터
            transform.position += localDirection;
        }

        if (isSKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.back * speed;
            Vector3 localDirection = -transform.forward * speed;

            // 월드좌표의 내 현재위치 + 방향벡터
            transform.position += localDirection;
        }

        if (isAKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.left * speed;
            Vector3 localDirection = -transform.right * speed;

            // 월드좌표의 내 현재위치 + 방향벡터
            transform.position += localDirection;
        }

        if (isDKeyDown)
        {
            // 방향 정하기
            Vector3 direction = Vector3.right * speed;
            Vector3 localDirection = transform.right * speed;

            // 월드좌표의 내 현재위치 + 방향벡터
            transform.position += localDirection;
        }
    }
}
