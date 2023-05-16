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

        // ped ��ġ�� ������ �߻��Ѵ�.
        // �� ����� results�� ��Ƴ���. (out�ε�)
        graphicRaycaster.Raycast(ped, results);

        if(results.Count <= 0)
        {
            return;
        }

        if (Input.GetMouseButton(0)) // ��� Ŭ�����̶�� �巡��
        {
            results[0].gameObject.transform.SetAsLastSibling();
            results[0].gameObject.transform.position = ped.position;
        }
    }
}
