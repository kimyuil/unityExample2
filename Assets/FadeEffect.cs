using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeEffect : MonoBehaviour
{
    private static FadeEffect instance;
    public static FadeEffect Instance => instance;

    public FadeEffect()
    {
        instance = this;
    }

    [SerializeField]
    Image fadeBackground;

    [SerializeField]
    [Range(0.01f, 10f)]
    float fadeTime;

    [SerializeField]
    AnimationCurve curve;

    AnimationCurve curveSave;

    [SerializeField]
    bool useCurve = true;

    [SerializeField]
    TextMeshProUGUI curveYn;

    bool isLoop = false;

    private void Awake()
    {
        
    }

    public void toggleCurve()
    {
        useCurve = !useCurve;
        curveYn.text = useCurve ? "curve On" : "curve Off";
    }

    public void FadeInOutAction()
    {
        StartCoroutine(FadeInOut(false));
    }

    public void FadeLoopAction()
    {
        isLoop = !isLoop;

        if (isLoop)
        {
            StartCoroutine("FadeInOut",true);
        }
        else
        {
            StopCoroutine("FadeInOut");
        }

    }


    public void FadeIn(float fadeTime = 1f)
    {

        this.fadeTime = fadeTime;

        StartCoroutine(Fade(1,0));


    }

    public void FadeOut(float fadeTime = 1f)
    {
        this.fadeTime = fadeTime;

        StartCoroutine(Fade(0, 1));
    }

    IEnumerator Fade(float start, float end)
    {
        float curruentTime = 0f;
        float percent = 0f;


        while ( percent < 1)
        {
            curruentTime += Time.deltaTime;
            percent = curruentTime / fadeTime;

            Color color = fadeBackground.color;
            
            color.a = Mathf.Lerp(start, end, useCurve ? curve.Evaluate(percent) : percent);
            fadeBackground.color = color;

            yield return null;
        }
        
    }

    IEnumerator FadeInOut(bool isLoop)
    {

        while (true)
        {            
            yield return StartCoroutine(Fade(0, 1));
            yield return StartCoroutine(Fade(1, 0));
            if (!isLoop)
            {
                break;
            }
        }

    }
}
