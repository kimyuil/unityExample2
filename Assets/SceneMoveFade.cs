using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveFade : SceneMove
{
    private void Start()
    {
        FadeEffect.Instance.FadeIn();
    }

    public override void MoveScene(string scene)
    {
        float time = 1f;
        FadeEffect.Instance.FadeOut(time);

        StartCoroutine(moveScene(time, scene));
    }

    IEnumerator moveScene(float time, string scene)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
}
