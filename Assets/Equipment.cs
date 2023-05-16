using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Equipment : MonoBehaviour
{

    [SerializeField]
    Image imageIcon;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemPrice;
    [SerializeField]
    TextMeshProUGUI itemDetail;

    public void Setup(EquipmentTemplate equipmentInfo)
    {
        itemName.text = equipmentInfo.name;
        itemName.color = GradeToColor(equipmentInfo.equipmentGrade);

        itemPrice.text = equipmentInfo.price.ToString();
        itemPrice.color = GradeToColor(equipmentInfo.equipmentGrade);

        imageIcon.sprite = Resources.Load<Sprite>(equipmentInfo.iconFile);

        if(itemDetail != null)
        {
            itemDetail.text = equipmentInfo.details; 
        }
    }

    public Color GradeToColor(EquipmentGrade grade)
    {
        switch (grade)
        {
            case EquipmentGrade.Normal:
                return Color.yellow;
            case EquipmentGrade.Magic:
                return Color.green;
            case EquipmentGrade.Rare:
                return Color.blue;
            case EquipmentGrade.Set:
                return Color.cyan;
            case EquipmentGrade.Unique:
                return Color.red;
            default:
                return Color.black;
        }
    }
}
