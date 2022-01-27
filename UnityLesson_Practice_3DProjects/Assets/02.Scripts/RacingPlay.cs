using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingPlay : MonoBehaviour
{
    #region 싱글톤패턴
    static public RacingPlay instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #endregion

    private List<PlayerMove> list_PlayerMove = new List<PlayerMove>();
    private List<Transform> list_PlayerFinished = new List<Transform>();
    public int totalPlayer = 0;
    [SerializeField] Transform goal;
    [SerializeField] List<Transform> list_WinPlatform = new List<Transform>();
    public bool onPlay;
    private void Update()
    {
        CheckPlayerReachedToGoalAndStopMove();
    }
    public void Register(PlayerMove playerMove)
    {
        list_PlayerMove.Add(playerMove);
        totalPlayer++;
        Debug.Log($"{playerMove.gameObject.name} (이)가 등록 완료 되었습니다, 현재 총 등록수 : {list_PlayerMove.Count}");
    }

    public void StartRacing()
    {
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            playerMove.doMove = true;
        }
        onPlay = true;
    }
    private void CheckPlayerReachedToGoalAndStopMove()
    {
        PlayerMove tmpFinishedPlayerMove = null;
        // check players are reached to goal
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            if(playerMove.transform.position.z > goal.position.z)
            {
                tmpFinishedPlayerMove = playerMove;
                break;
            }
        }
        // when player is reached to goal.
        if(tmpFinishedPlayerMove != null)
        {
            tmpFinishedPlayerMove.doMove = false;
            list_PlayerFinished.Add(tmpFinishedPlayerMove.transform);
            list_PlayerMove.Remove(tmpFinishedPlayerMove);
        }
        // when finish (all player are reached to goal)
        if(list_PlayerFinished.Count == totalPlayer)
        {
            for (int i = 0; i < list_WinPlatform.Count; i++)
            {
                list_PlayerFinished[i].position = list_WinPlatform[i].position;
            }
            CameraHandler.instance.SetCamToPlatform();
        }
    }

    public Transform Get1GradePlayerTransform()
    {
        Transform leader = list_PlayerMove[0].gameObject.GetComponent<Transform>();
        float prevDistance = list_PlayerMove[0].distance;
        foreach (PlayerMove playerMove in list_PlayerMove)
        {
            if (playerMove.distance > prevDistance)
            {
                prevDistance = playerMove.distance;
                leader = playerMove.gameObject.GetComponent<Transform>();
            }
        }
        return leader;
    }
    
    public Transform GetPlayerTransform(int index)
    {
        if (index < list_PlayerMove.Count)
        {
            Transform tmpPlayerTransform = list_PlayerMove[index].transform;
            return tmpPlayerTransform;
        }
        else
        {
            return null;
        }
    }

}