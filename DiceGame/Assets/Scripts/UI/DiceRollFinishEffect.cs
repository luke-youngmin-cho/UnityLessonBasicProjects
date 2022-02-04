using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollFinishEffect : MonoBehaviour
{
    [SerializeField] private float lastingTime;
    [SerializeField] private float rotateSpeed;
    float timeLapse = 0;
    private void OnEnable()
    {
        timeLapse = 0;
    }

    private void Update()
    {
        if(timeLapse > lastingTime)
            this.gameObject.SetActive(false);

        timeLapse += Time.deltaTime;
        this.transform.Rotate(new Vector3(0f, 0f, rotateSpeed));
    }
}
