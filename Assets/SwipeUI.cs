using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{

    [SerializeField]
    Scrollbar scrollbar;

    [SerializeField]
    Transform[] circleContents;
    float circleFocusScale = 1.3f;

    [SerializeField]
    float swipeTime = 0.2f; // ���������Ǵ� �ð�

    [SerializeField]
    float swipeDistance = 50f; // ���������� ���� ������ �ּ� �Ÿ�

    float[] scrollPageValues; // �� �������� ��ġ��. 0~1 �� �迭�̴�.
    float valueDistance; // �� ������ ������ �Ÿ�
    int currentPage = 0;
    int maxPage;
    float startTouchY; // ��ġ���� y��
    float endTouchY; // ��ġ�� Y��
    bool isSwipe = false;

    private void Awake()
    {
        scrollPageValues = new float[transform.childCount];
        valueDistance = 1f / (scrollPageValues.Length - 1f);

        for(int i = 0; i < scrollPageValues.Length; i++)
        {
            scrollPageValues[i] = valueDistance * i;
        }
        maxPage = scrollPageValues.Length;
    }

    void Start()
    {
        SetScrollBarValue(maxPage-1);
    }

    public void SetScrollBarValue(int index)
    {
        currentPage = index;
        scrollbar.value = scrollPageValues[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwipe) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {            
            startTouchY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endTouchY = Input.mousePosition.y;
            UpdateSwipe();
        }

#endif

        UpdateCircleContent();
    }

    void UpdateSwipe()
    {
        if(Mathf.Abs(startTouchY - endTouchY) < swipeDistance)
        {
            StartCoroutine(SwipeOneStep(currentPage));
            // �ڷ�ƾ���� �ǵ��ư��� ������ �ʿ�.
            return;
        }

        bool isDown = startTouchY > endTouchY;
        if (isDown)
        {
            if (currentPage == maxPage - 1) return;
            currentPage++;
        }
        else
        {
            if (currentPage == 0) return;
            currentPage--;
        }
        // �ڷ�ƾ���� ã�ư��� ������ �ʿ�..
        StartCoroutine(SwipeOneStep(currentPage));
    }

    void UpdateCircleContent()
    {
        int reverse = scrollPageValues.Length-1;
        for (int i = 0; i < scrollPageValues.Length; i++)
        {
            circleContents[reverse-i].localScale = Vector2.one;
            circleContents[reverse-i].GetComponent<Image>().color = Color.white;

            // ���� ������ �Ѿ�� �ٲٰ�
            if (scrollbar.value < scrollPageValues[i] + (valueDistance / 2) &&
                scrollbar.value > scrollPageValues[i] - (valueDistance / 2))
            {
                circleContents[reverse-i].localScale = Vector2.one * circleFocusScale;
                circleContents[reverse - i].GetComponent<Image>().color = Color.black;
            }
        }   

    

    }

    IEnumerator SwipeOneStep(int index)
    {
        // ������ġ����, scrollPageValues[index] ��, swipeTime �ð����� ���� �־��شٴ� ��..

        isSwipe = true;

        float current = 0;
        float percent = 0;
        float start = scrollbar.value;
        float end = scrollPageValues[index];

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / swipeTime;

            scrollbar.value = Mathf.Lerp(start, end, percent);
            yield return null;
        }

        isSwipe = false;
    }
}
