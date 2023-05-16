using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform beforeParent;

    Transform canvas;    // ui�� �Ҽӵ� �ֻ���� canvas�� transform
    RectTransform rect; // �ڱ��ڽ��� rectTransform
    CanvasGroup canvasGroup; //ui�� ���İ��� ��ȣ�ۿ� ��� ���ѰŶ�� ��.

    private void Awake()
    {        
        canvas = FindObjectOfType<Canvas>().transform; // �̰� ��Ȯ�� �������� �𸣰ڴ�.
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParent = transform.parent;
        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ������ ������ �巡�׸� ������ ���
        if( transform.parent == canvas)
        {
            // �θ�, ��ġ�� ����ġ��
            transform.SetParent(beforeParent);
            rect.position = beforeParent.GetComponent<RectTransform>().position;
        }

        // ���ο� ���� �θ����� OnDrop���� �����ǳ�����.
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
