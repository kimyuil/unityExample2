using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NavigationView : MonoBehaviour
{
    // 네비게이션 뷰와 네비게이션 바에 해당하는 컴포넌트를 모두 받는다.
    [SerializeField]
    RectTransform curruentView; // 현재 view를 일단 받아놓고 시작하는 것.

    [SerializeField]
    TextMeshProUGUI textTitle;

    [SerializeField]
    Button prevButton;

    [SerializeField]
    TextMeshProUGUI prevText;

    CanvasGroup canvasGroup; // 충돌처리 on off를 위해 사용한다.
    Stack<RectTransform> stackViews; // 네비게이션 뷰의 정보들을 stack에 저장하고 관리한다. 근데 RectTransform을 넣는구나..

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        stackViews = new Stack<RectTransform>();

        prevButton.onClick.AddListener(Pop);
        prevButton.gameObject.SetActive(false); // 맨 처음은 최상위 뷰이니 ACTIVE는 false;

        SetNavigationBar(curruentView.name);
    }

    public void Push(RectTransform newView)
    {
        // stack에 푸시 및 화면 전환

        // 이동하는 동안 raycast 동작 안하도록 설정해주어야 한다.
        canvasGroup.blocksRaycasts = false;

        // 사라지는 뷰.
        RectTransform prevView = curruentView;
        prevView.gameObject.SetActive(false);
        stackViews.Push(prevView);

        // 생기는 뷰
        curruentView = newView;
        curruentView.gameObject.SetActive(true);

        canvasGroup.blocksRaycasts = true;

        SetNavigationBar(curruentView.name, prevView.name);
    }

    public void Pop()
    {
        if (stackViews.Count == 0) return;

        // 이전 뷰로 돌아가는 함수
        canvasGroup.blocksRaycasts = false;

        curruentView.gameObject.SetActive(false);

        curruentView = stackViews.Pop();
        curruentView.gameObject.SetActive(true);

        canvasGroup.blocksRaycasts = true;

        if(stackViews.Count >= 1)
        {
            SetNavigationBar(curruentView.name, stackViews.Peek().name);
        }
        else
        {
            SetNavigationBar(curruentView.name);
        }
        
    }

    void SetNavigationBar(string currentName, string prevViewName = "")
    {
        // 네비게이션 바에 이름설정
        textTitle.text = currentName;

        if (prevViewName.Equals(""))
        {
            prevButton.gameObject.SetActive(false);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
            prevText.text = prevViewName;
        }

    }

}
