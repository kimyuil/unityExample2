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
        // Ű����� �Է�
        if (Input.GetKeyDown("1"))
        {
            SkillCooldown.StartSkill();
        }

        // ���콺�� Ŭ��
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
