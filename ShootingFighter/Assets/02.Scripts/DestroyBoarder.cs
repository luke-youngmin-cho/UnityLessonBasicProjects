using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoarder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
