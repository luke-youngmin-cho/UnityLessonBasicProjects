using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisAfterTime : MonoBehaviour
{
    [SerializeField] float destroyDelay;
    public void OnEnable()
    {
        Destroy(this.gameObject, destroyDelay);
    }
}
