using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField]
    Image skillCoverImage;

    [SerializeField]
    TextMeshProUGUI skillCoverText;

    [SerializeField]
    TextMeshProUGUI skillInfoText;

    [SerializeField]
    string skillName;

    [SerializeField]
    float coolTime;

    float curruentCoolTime;

    bool skillActive = true;

    void Start()
    {
        SetCoolStatus(false);
    }

    public void StartSkill()
    {

        if (skillActive)
        {
            skillInfoText.text = $"{skillName} start.";
            StartCoroutine("SkillCoolProcess");
        }
        else
        {
            skillInfoText.text = $"wait for {curruentCoolTime.ToString("F1")} seconds..";
        }
    }

    IEnumerator SkillCoolProcess()
    {
        curruentCoolTime = coolTime;
        skillActive = false;
        SetCoolStatus(true);
        while (curruentCoolTime >= 0f)
        {
            skillCoverImage.fillAmount = curruentCoolTime / coolTime;
            skillCoverText.text = curruentCoolTime.ToString("F1");

            curruentCoolTime -= Time.deltaTime;
            yield return null;
        }
        SetCoolStatus(false);
        skillActive = true;
        skillInfoText.text = "";
    }

    
    

    private void SetCoolStatus(bool start)
    {
        skillCoverImage.enabled = start;
        skillCoverText.enabled = start;        
    }
}
