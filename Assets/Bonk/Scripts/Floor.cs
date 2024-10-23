using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private List<Tile> tilesList = new List<Tile>();
    [SerializeField]
    private TextMeshPro text;
    private int floorNubmer;
    public int FloorNumber => floorNubmer;
    private List<int> bombTileNumbersList = new List<int>();

    /// <summary>
    /// ランダムなタイルに任意の数爆弾をセット
    /// </summary>
    /// <param name="bombCount"></param>
    public void InitFloor(int floorNumber,int bombCount)
    {
        this.floorNubmer = floorNumber;
        text.text = $"Floor {floorNubmer}";
        GetBombTileNumber(bombCount);
        if (tilesList.Count != Constant.TILE_COUNT)
        {
            Debug.Log("タイルが正しくアサインされていません");
            return;
        }

        for (int i = 0; i < tilesList.Count; i++)
        {
            // 爆弾タイルの場合
            if (bombTileNumbersList.Contains(i))
            {
                tilesList[i].InitTile(floorNumber:floorNumber,isBomb:true);
            }
            else
            {
                tilesList[i].InitTile(floorNumber: floorNumber, isBomb: false);
            }
        }
        //Debug.Log($"{floorNumber}階の爆弾の位置は{string.Join(",", bombTileNumbersList)}");
    }

    /// <summary>
    /// 爆弾の位置を決める
    /// </summary>
    /// <param name="bombCount"></param>
    private void GetBombTileNumber(int bombCount)
    {
        bombTileNumbersList.Clear();
        List<int> numbers = new List<int>();
        for (int i = 0; i < Constant.TILE_COUNT; i++)
        {
            numbers.Add(i);
        }
        // 爆弾の数だけ、ランダムにタイルに埋め込んでいく
        while (bombCount-- > 0)
        {
            int randomIndex = Random.Range(0, numbers.Count);
            bombTileNumbersList.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex);
        }
    }
}
