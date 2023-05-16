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
    Transform equipmentParent; // 장비 [[버튼]] 오브젝트가 배치될 부모 (Contents)

    [SerializeField]
    List<EquipmentTemplate> equipments; // 동적으로 변할 수 있기에 배열이 아닌 List를 사용.

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
