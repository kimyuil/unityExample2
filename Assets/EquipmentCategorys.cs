using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCategorys : MonoBehaviour
{
    [SerializeField]
    NavigationView navigationView;

    [SerializeField]
    RectTransform detailRectTransform;

    [SerializeField]
    GameObject equipmentPrefab;

    [SerializeField]
    Transform equipmentParent; // ��� [[��ư]] ������Ʈ�� ��ġ�� �θ� (Contents)

    [SerializeField]
    List<EquipmentTemplate> equipments; // �������� ���� �� �ֱ⿡ �迭�� �ƴ� List�� ���.

    private void Awake()
    {
        foreach(var equip in equipments)
        {
            var clone = Instantiate(equipmentPrefab, equipmentParent);
            clone.GetComponent<Equipment>().Setup(equip);
            clone.GetComponent<EquipmentButton>().Setup(navigationView, equip, detailRectTransform);
        }        
    }
}
