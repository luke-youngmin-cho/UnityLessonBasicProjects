using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Bonus : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayManager.instance.IncreaseDiceNumber();
        Debug.Log($"TileEvent - Bonus : You place on bonus tile so you can get 1 additional dice");
    }
}
