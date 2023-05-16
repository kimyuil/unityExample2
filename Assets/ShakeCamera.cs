using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    private static ShakeCamera instance;
    public static ShakeCamera Instance => instance;

    private float shakeTime;
    private float shakeForce;

    private Vector3 beforePosition;
    private Vector3 beforeRotation;

    public ShakeCamera()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        beforePosition = transform.position;
        beforeRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            ShakeProcess(0.5f, 0.05f);
        }
        if (Input.GetKeyDown("2"))
        {
            RotationProcess(0.5f, 0.1f);
        }
    }*/

    // 이 함수를 외부에서 호출 가능하게 public으로 빼준다는 것.
    public void ShakeProcess(float shakeTime = 1f, float shakeForce = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeForce = shakeForce;
        
        StopCoroutine("ShakeByPosition"); // 키를 마구 연타하면 원위치로 돌아오지 못한 상태가 되더라.
        transform.position = beforePosition;
        StartCoroutine("ShakeByPosition");
    }

    private IEnumerator ShakeByPosition()
    {
        // 왜 여기로 못넘어올까? IEnumerator 인데 IEnumerable 로 리턴해서.. ㅜㅜㅜ
        beforePosition = transform.position;
        while (shakeTime >= 0)
        {
            // 구 의 범위 * 흔드는 힘만큼 transform을 이동!            
            transform.position = beforePosition + Random.insideUnitSphere * shakeForce;            
            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.position = beforePosition;

    }

    public void RotationProcess(float shakeTime = 1f, float shakeForce = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeForce = shakeForce;

        StopCoroutine("ShakeByRotation");
        transform.eulerAngles = beforeRotation;
        StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByRotation()
    {
        float x = 0;
        float y = 0;
        float power = 30f;

        beforeRotation = transform.eulerAngles;
        while (shakeTime >= 0)
        {

            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(beforeRotation + new Vector3(x, y, z) * shakeForce * power);
            
            shakeTime -= Time.deltaTime;

            yield return null; 
        }

        transform.eulerAngles = beforeRotation;

    }

}
