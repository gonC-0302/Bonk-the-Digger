using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private bool isBomb;
    public bool IsBomb => isBomb;
    private int floorNumber;
    public int FloorNumber => floorNumber;

    public void InitTile(int floorNumber,bool isBomb)
    {
        gameObject.SetActive(true);
        this.floorNumber = floorNumber;
        this.isBomb = isBomb;
        // デバッグ用
        if (isBomb) GetComponent<SpriteRenderer>().color = Color.red;
        else GetComponent<SpriteRenderer>().color = Color.white;
    }

    /// <summary>
    /// 非アクティブ化する
    /// </summary>
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
