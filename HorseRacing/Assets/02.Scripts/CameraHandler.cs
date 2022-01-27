using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    static public CameraHandler instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    Transform tr;
    int targetIndex = 0;
    Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] Transform gradePlatform;
    // Start is called before the first frame update

    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RacingPlay.instance.onPlay == false) return;

        if (Input.GetKeyDown("tab"))
            SwitchNextTarget();

        if (target == null)
            SwitchNextTarget();
        else
            tr.position = target.position + offset;
    }

    public void SwitchTarget(int index)
    {
        if (index > RacingPlay.instance.totalPlayer - 1) return;
        target = RacingPlay.instance.GetPlayerTransform(index);
    }
    public void SwitchNextTarget()
    {
        targetIndex++;
        if (targetIndex > RacingPlay.instance.totalPlayer - 1)
            targetIndex = 0;
        target = RacingPlay.instance.GetPlayerTransform(targetIndex);
    }
    public void SwitchTargetTo1Grade()
    {
        target = RacingPlay.instance.Get1GradePlayerTransform();
    }

    public void SetCamToPlatform()
    {
        tr.position = gradePlatform.position;
        tr.rotation = gradePlatform.rotation;
    }
}
