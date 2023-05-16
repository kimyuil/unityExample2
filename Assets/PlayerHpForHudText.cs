using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHpForHudText : MonoBehaviour
{
    [SerializeField]
    GameObject hudTextPrefab;

    // text ui가 배치될 부모의 transform
    [SerializeField]
    Transform parentTransform;

    private void OnEnable()
    {
        StartCoroutine("UpdateHpLoop");
    }

    private void OnDisable()
    {
        StopCoroutine("UpdateHpLoop");
    }

    IEnumerator UpdateHpLoop()
    {
        while (true)
        {
            float time = Random.Range(0.1f, 1f);

            yield return new WaitForSeconds(time);

            int type = Random.Range(0,2); // 0은 체력증가 1은 체력감소
            string text = Random.Range(10, 1000).ToString(); // 증감의 양            
            Color color = type == 0 ? Color.green : Color.red; // 컬러색깔

            var clone = Instantiate(hudTextPrefab);
            clone.transform.SetParent(parentTransform);
            Bounds bounds = GetComponent<CapsuleCollider2D>().bounds;
            clone.GetComponent<UIHUDText>().Play(text, color, bounds);

        }
    }


}
