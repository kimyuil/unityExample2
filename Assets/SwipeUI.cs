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
    float swipeTime = 0.2f; // 스와이프되는 시간

    [SerializeField]
    float swipeDistance = 50f; // 스와이프를 위해 움직일 최소 거리

    float[] scrollPageValues; // 각 페이지의 위치값. 0~1 의 배열이다.
    float valueDistance; // 각 페이지 사이의 거리
    int currentPage = 0;
    int maxPage;
    float startTouchY; // 터치시작 y점
    float endTouchY; // 터치끝 Y점
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
            // 코루틴으로 되돌아가는 로직이 필요.
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
        // 코루틴으로 찾아가는 로직이 필요..
        StartCoroutine(SwipeOneStep(currentPage));
    }

    void UpdateCircleContent()
    {
        int reverse = scrollPageValues.Length-1;
        for (int i = 0; i < scrollPageValues.Length; i++)
        {
            circleContents[reverse-i].localScale = Vector2.one;
            circleContents[reverse-i].GetComponent<Image>().color = Color.white;

            // 원은 절반을 넘어가면 바꾸게
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
        // 현재위치에서, scrollPageValues[index] 로, swipeTime 시간동안 값을 넣어준다는 것..

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
