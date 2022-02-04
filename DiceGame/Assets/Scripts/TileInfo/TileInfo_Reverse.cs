using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Reverse : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        if (DicePlayManager.instance != null)
            DicePlayUI.instance.SwitchDicePanel();
        Debug.Log($"TileEvent - Reverse : You place on reverse tile. next dice roll will move player opposite way");
    }
}
