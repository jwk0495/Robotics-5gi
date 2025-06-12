using UnityEngine;

// 시곗바늘을 2초 동안 회전시킨다.
// 속성: 시간
public class LerpRotation : MonoBehaviour
{
    public float time;
    public float elapsedTime;
    public float startAngle;
    public float endAngle;
    Quaternion startQ;
    Quaternion endQ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startQ = Quaternion.AngleAxis(startAngle, transform.forward);
        endQ = Quaternion.AngleAxis(endAngle, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > time)
            elapsedTime = 0;

        Quaternion q = Quaternion.Lerp(startQ, endQ, elapsedTime / time);

        transform.rotation = q;
    }
}
