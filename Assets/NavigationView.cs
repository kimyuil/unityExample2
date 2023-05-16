using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NavigationView : MonoBehaviour
{
    // �׺���̼� ��� �׺���̼� �ٿ� �ش��ϴ� ������Ʈ�� ��� �޴´�.
    [SerializeField]
    RectTransform curruentView; // ���� view�� �ϴ� �޾Ƴ��� �����ϴ� ��.

    [SerializeField]
    TextMeshProUGUI textTitle;

    [SerializeField]
    Button prevButton;

    [SerializeField]
    TextMeshProUGUI prevText;

    CanvasGroup canvasGroup; // �浹ó�� on off�� ���� ����Ѵ�.
    Stack<RectTransform> stackViews; // �׺���̼� ���� �������� stack�� �����ϰ� �����Ѵ�. �ٵ� RectTransform�� �ִ±���..

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        stackViews = new Stack<RectTransform>();

        prevButton.onClick.AddListener(Pop);
        prevButton.gameObject.SetActive(false); // �� ó���� �ֻ��� ���̴� ACTIVE�� false;

        SetNavigationBar(curruentView.name);
    }

    public void Push(RectTransform newView)
    {
        // stack�� Ǫ�� �� ȭ�� ��ȯ

        // �̵��ϴ� ���� raycast ���� ���ϵ��� �������־�� �Ѵ�.
        canvasGroup.blocksRaycasts = false;

        // ������� ��.
        RectTransform prevView = curruentView;
        prevView.gameObject.SetActive(false);
        stackViews.Push(prevView);

        // ����� ��
        curruentView = newView;
        curruentView.gameObject.SetActive(true);

        canvasGroup.blocksRaycasts = true;

        SetNavigationBar(curruentView.name, prevView.name);
    }

    public void Pop()
    {
        if (stackViews.Count == 0) return;

        // ���� ��� ���ư��� �Լ�
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
        // �׺���̼� �ٿ� �̸�����
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
