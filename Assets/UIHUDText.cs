using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHUDText : MonoBehaviour
{
    [SerializeField]
    float moveDistance = 100; // 해당 text가 움직일 거리. Recttransform 기준

    [SerializeField]
    float moveTime = 1.5f; // 이동시간

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

    // gap은 머리위에 살짝 공간.. 그 위에서부터 시작하려고.
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

            // 투명도도 제어하기
            Color color = this.text.color;
            color.a = Mathf.Lerp(1, 0, percent);
            this.text.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }

}
