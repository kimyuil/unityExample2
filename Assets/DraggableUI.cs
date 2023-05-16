using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform beforeParent;

    Transform canvas;    // ui가 소속된 최상단의 canvas의 transform
    RectTransform rect; // 자기자신의 rectTransform
    CanvasGroup canvasGroup; //ui의 알파값과 상호작용 제어를 위한거라고 함.

    private void Awake()
    {        
        canvas = FindObjectOfType<Canvas>().transform; // 이게 정확히 무엇인지 모르겠다.
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
        // 엉뚱한 곳에서 드래그를 끝났을 경우
        if( transform.parent == canvas)
        {
            // 부모도, 위치도 원위치로
            transform.SetParent(beforeParent);
            rect.position = beforeParent.GetComponent<RectTransform>().position;
        }

        // 새로운 곳의 부모세팅은 OnDrop에서 구현되나보다.
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
