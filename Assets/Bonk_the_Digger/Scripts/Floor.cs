using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private List<Tile> tilesList = new List<Tile>();
    [SerializeField]
    private TextMeshPro floorNumberText;
    [SerializeField]
    private SpriteRenderer background;
    private int floorNubmer;
    public int FloorNumber => floorNubmer;
    private List<int> bombTileNumbersList = new List<int>();
    private int bonusTileNumber;

    public void SetFloorNumber(int floorNumber)
    {
        this.floorNubmer = floorNumber;
        if (floorNubmer == 0)
        {
            background.enabled = false;
            floorNumberText.text = "";
        }
        else
        {
            background.enabled = true;
            floorNumberText.text = $"{floorNubmer}";
        }
    }

    /// <summary>
    /// フロア内のタイルに爆弾を設置する
    /// </summary>
    /// <param name="bombCount"></param>
    /// <exception cref="System.Exception"></exception>
    // TODO: リファクタリング必要
    public void SetTiles(int bombCount)
    {
        if (tilesList.Count != Constant.TILE_COUNT)
        {
            throw new System.Exception("タイルのアサインで例外発生");
        }

        List<int> numbers = new List<int>();
        for (int i = 0; i < Constant.TILE_COUNT; i++)
        {
            numbers.Add(i);
        }

        // 爆弾タイルとボーナスタイルの番号を決める
        var remainingNumbers = DetermineBombTilePosition(numbers, bombCount);
        //Debug.Log($"階層{floorNubmer}の爆弾タイル：{string.Join(",", bombTileNumbersList)}");
        if (floorNubmer != 0 && floorNubmer % 5 == 0)
        {
            DetermineBonusTilePosition(remainingNumbers);
            //Debug.Log($"階層{floorNubmer}のボーナスタイル：{bonusTileNumber}");
        }

        // 爆弾タイルとボーナスタイルを設定する
        for (int i = 0; i < tilesList.Count; i++)
        {
            // 5の倍数の階層
            if (floorNubmer != 0 && floorNubmer % 5 == 0)
            {
                // 爆弾タイル
                if (bombTileNumbersList.Contains(i))
                {
                    tilesList[i].InitTile(floorNumber: floorNubmer, type: TileType.Bomb);
                }
                else
                {
                    // ボーナスタイル
                    if (i == bonusTileNumber)
                    {
                        tilesList[i].InitTile(floorNumber: floorNubmer, type: TileType.ChallengeBox);
                    }
                    else
                    {
                        tilesList[i].InitTile(floorNumber: floorNubmer, type: TileType.Normal);
                    }
                }
            }
            else
            {
                // 爆弾タイルの場合
                if (bombTileNumbersList.Contains(i))
                {
                    tilesList[i].InitTile(floorNumber: floorNubmer, type: TileType.Bomb);
                }
                // 普通のタイル
                else
                {
                    tilesList[i].InitTile(floorNumber: floorNubmer, type: TileType.Normal);
                }
            }
        }

    }

    /// <summary>
    /// 爆弾の位置を決める
    /// </summary>
    /// <param name="bombCount"></param>
    private List<int> DetermineBombTilePosition(List<int> numbers,int bombCount)
    {
        bombTileNumbersList.Clear();
        // 爆弾の数だけ、ランダムにタイルに埋め込んでいく
        while (bombCount-- > 0)
        {
            // 0 ~ 4でランダムにIndexを取得
            int randomIndex = Random.Range(0, numbers.Count);
            // 爆弾タイルのインデックスを追加
            bombTileNumbersList.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex);
        }
        return numbers;
      
    }
    /// <summary>
    /// ボーナスタイルの位置を決める
    /// </summary>
    /// <param name="numbers"></param>
    private void DetermineBonusTilePosition(List<int> numbers)
    {
        int bonusTileIndex = Random.Range(0, numbers.Count);
        bonusTileNumber = numbers[bonusTileIndex];
    }
}
