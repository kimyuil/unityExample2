using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{    
    public virtual void MoveScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
