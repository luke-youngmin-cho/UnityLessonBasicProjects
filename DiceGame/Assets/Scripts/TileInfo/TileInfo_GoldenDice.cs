using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_GoldenDice : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        DicePlayManager.instance.IncreaseGoldenDiceNumber();
        Debug.Log($"TileEvent - Golden Dice : You place on golden dice tile. you'll get additional 1 golden dice!");
    }
}
