using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphicRaycasterScript : MonoBehaviour
{
    GraphicRaycaster graphicRaycaster;
    // Start is called before the first frame update
    void Awake()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        PointerEventData ped = new PointerEventData(null);

        ped.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        // ped 위치로 광선을 발사한다.
        // 그 결과를 results로 담아낸다. (out인듯)
        graphicRaycaster.Raycast(ped, results);

        if(results.Count <= 0)
        {
            return;
        }

        if (Input.GetMouseButton(0)) // 계속 클릭중이라면 드래그
        {
            results[0].gameObject.transform.SetAsLastSibling();
            results[0].gameObject.transform.position = ped.position;
        }
    }
}
