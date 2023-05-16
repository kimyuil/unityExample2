using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public void move(Vector3 direction)
    {
        transform.position += direction;
    }
}
