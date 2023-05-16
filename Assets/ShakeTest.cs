using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour
{
    public void ShakePosition()
    {
        ShakeCamera.Instance.ShakeProcess(0.5f, 0.05f);
    }

    public void ShakeRotation()
    {
        ShakeCamera.Instance.RotationProcess(0.5f, 0.1f);
    }
}
