using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUse : MonoBehaviour
{
    [SerializeField]
    SkillCooldown SkillCooldown;

    [SerializeField]
    GraphicRaycaster graphicRaycaster;

    List<RaycastResult> results;
    private void Awake()
    {
        results = new List<RaycastResult>();
    }

    void Update()
    {
        // 키보드로 입력
        if (Input.GetKeyDown("1"))
        {
            SkillCooldown.StartSkill();
        }

        // 마우스로 클릭
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            graphicRaycaster.Raycast(ped, results);
            if (results.Count > 0)
            {
                var skillCool = results[0].gameObject.GetComponent<SkillCooldown>();
                if(skillCool != null)
                {
                    skillCool.StartSkill();
                }                
            }            
        }
    }
}
