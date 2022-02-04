using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileInfo_Star : TileInfo
{
    public int starValue
    {
        set
        {
            _starValue = value;
            if (starValueText != null)
            {
                starValueText.text = _starValue.ToString();
            }
        }
        get
        {
            return _starValue;
        }
    }
    private int _starValue = (int)GameSettingsData.INIT_STAR_TILE_VALUE;
    [SerializeField] Text starValueText;
    public override void TileEvent()
    {
        base.TileEvent();
        starValue = starValue + 1;        
        Debug.Log($"TileEvent - Star : You place on Star-Tile and star value increased as {starValue}");
    }
}
