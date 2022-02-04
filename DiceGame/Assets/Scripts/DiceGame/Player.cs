using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Move(Vector3 to)
    {
        this.transform.position = to;
    }
}
