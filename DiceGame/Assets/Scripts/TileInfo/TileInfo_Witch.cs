using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo_Witch : TileInfo
{
    public override void TileEvent()
    {
        base.TileEvent();
        Debug.Log($"TileEvent - Witch : ???");
    }
}
