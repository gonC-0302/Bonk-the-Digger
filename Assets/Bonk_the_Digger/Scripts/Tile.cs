using NUnit.Framework.Interfaces;
using UnityEngine;
using DG.Tweening;

public enum TileType
{
    Normal,
    Bomb,
    Bonus,
}

public class Tile : MonoBehaviour
{
    private int floorNumber;
    public int FloorNumber => floorNumber;
    private TileType type;
    public TileType Type => type;
    [SerializeField]
    private SpriteRenderer tileSpr;
    [SerializeField]
    private SpriteRenderer itemSpr;
    [SerializeField]
    private TileItemDataSO itemData;
    [SerializeField]
    private Sprite explosionEffect;
    [SerializeField]
    private SpriteRenderer redScreenFilter;
    private int tapCount;
    public int TapCount => tapCount;

    public void InitTile(int floorNumber,TileType type)
    {
        this.floorNumber = floorNumber;
        this.type = type;
        tapCount = 0;
        tileSpr.enabled = true;
        itemSpr.enabled = false;
        itemSpr.sprite = itemData.itemsList.Find(x => x.type == type).spr;
        switch (type)
        {
            case TileType.Normal:
                tileSpr.color = Color.white;
                break;
            case TileType.Bomb:
                tileSpr.color = Color.red;

                break;
            case TileType.Bonus:
                tileSpr.color = Color.yellow;
                break;
        }
    }
    public void UpdateTapCount()
    {
        tapCount++;
        gameObject.transform.DOScale(1.4f, 0.1f)
            .OnComplete(() => gameObject.transform.DOScale(1.2f,0.1f));
    }

    public void OpenTile()
    {
        tileSpr.enabled = false;
        itemSpr.enabled = true;
    }
    public void GetItem()
    {
        itemSpr.enabled = false;
    }

    /// <summary>
    /// 爆弾を爆発させる
    /// </summary>
    public void ExplodeBomb()
    {
        itemSpr.transform.DOScale(0.15f, 0.05f).SetLoops(10, LoopType.Yoyo)
            .OnComplete(() =>
            {
                itemSpr.sprite = explosionEffect;
                itemSpr.transform.DOScale(0.4f, 0.1f);
                redScreenFilter.enabled = true;
                redScreenFilter.transform.DOScale(200, 0.5f);
            });
    }
}
