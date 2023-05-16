using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAtoB : MonoBehaviour
{
    [SerializeField]
    Vector2[] movePoints;

    private int curIndex = 0;

    private void OnEnable()
    {
        StartCoroutine("MoveLoop");
    }

    private void OnDisable()
    {
        StopCoroutine("MoveLoop");
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return StartCoroutine("MoveTo");
        }
    }

    IEnumerator MoveTo()
    {
        float current = 0;
        float percent = 0;

        Vector2 start = transform.position;
        Vector2 end = movePoints[curIndex];

        // �Ÿ��� ����ؼ� �̵��ð��� �þ���� ����.. 1�̵��� 1�ʰ� �ҿ�ǰ�..?
        float time = Vector2.Distance(start, end);

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            transform.position = Vector3.Lerp(start, end, percent);
            yield return null;
        }

        curIndex = curIndex < movePoints.Length - 1 ? curIndex + 1 : 0;
    }
}
