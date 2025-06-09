using UnityEngine;

// Unity의 Lifecycle 메서드
// 물체를 방향키의 입력을 받아 이동시킨다.
public class MovePlayer : MonoBehaviour
{
    public float speed = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("살아남");
    }

    // 100FPS: 1초에 100번 업데이트 함수가 작동
    // 30FPS ~ 400FPS
    // Update is called once per frame
    void Update()
    {
        // MoveWithoutTime();

        MoveWithTime();
    }

    private void MoveWithTime()
    {
        // 조이스틱의 인풋을 모방한 -1 ~ 1을 표현하는 함수
        float horizontalInput = Input.GetAxis("Horizontal"); // 방향키 좌우
        float verticalInput = Input.GetAxis("Vertical");     // 방향키 상하

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        transform.position += direction * speed * Time.deltaTime; // 0.3s 
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
            Vector3 direction = Vector3.forward * speed; // 월드 좌표계 기준
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
