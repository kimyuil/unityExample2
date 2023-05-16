using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentButton : MonoBehaviour
{
    EquipmentTemplate equipInfo;
    NavigationView navigationView;
    RectTransform rectChild;

    public void Setup(NavigationView navigationView, EquipmentTemplate equipInfo, RectTransform detail)
    {
        this.navigationView = navigationView;
        this.equipInfo = equipInfo;
        this.rectChild = detail;

        GetComponent<Button>().onClick.AddListener(equipmentButtonClick);
    }

    public void equipmentButtonClick()
    {
        rectChild.GetComponent<Equipment>().Setup(equipInfo);
        navigationView.Push(rectChild);
    }
}
