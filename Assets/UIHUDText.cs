using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUDText : MonoBehaviour
{
    [SerializeField]
    float moveDistance = 100; // �ش� text�� ������ �Ÿ�. Recttransform ����

    [SerializeField]
    float moveTime = 1.5f; // �̵��ð�

    private RectTransform rectTransform;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

    // gap�� �Ӹ����� ��¦ ����.. �� ���������� �����Ϸ���.
    public void Play(string text, Color color, Bounds bounds, float gap=0.1f)
    {        
        this.text.text = text;
        this.text.color = color;        
        StartCoroutine(MoveText(bounds, gap));
    }

    IEnumerator MoveText(Bounds bounds, float gap)
    {
        Vector2 start = Camera.main.WorldToScreenPoint(new Vector3(bounds.max.x, bounds.max.y + gap, bounds.center.z));
        Vector2 end = start + Vector2.up * moveDistance;

        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;
            rectTransform.position = Vector3.Lerp(start, end, percent);

            // ������ �����ϱ�
            Color color = this.text.color;
            color.a = Mathf.Lerp(1, 0, percent);
            this.text.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }

}
