using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public int index;
    public string name;
    public string discription;
    public delegate void OnTile();
    public OnTile onTile;

    protected TileInfo()
    {
        onTile = TileEvent;
    }
    virtual public void TileEvent()
    {
        Debug.Log($"Tile number {index}, The Player is on {name}, {discription}");
    }
}
