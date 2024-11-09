using NUnit.Framework.Interfaces;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public enum TileType
{
    Normal,
    Bomb,
    Bonus,
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer tileSpr;
    [SerializeField]
    private SpriteRenderer itemSpr;
    [SerializeField]
    private Sprite explosionEffect,treasure;
    [SerializeField]
    private SpriteRenderer redScreenFilter;
    [SerializeField]
    private TextMeshPro rateText;
    private int floorNumber;
    public int FloorNumber => floorNumber;
    private TileType type;
    public TileType Type => type;
    private int tapCount;
    public int TapCount => tapCount;

    public void InitTile(int floorNumber,TileType type)
    {
        this.floorNumber = floorNumber;
        this.type = type;
        tapCount = 0;
        ResetRateText();
        itemSpr.transform.localScale = Vector3.one * 0.125f;
        tileSpr.enabled = true;
        itemSpr.enabled = false;
        itemSpr.sprite = DataBaseManager.instance.tileDataSO.itemsList.Find(x => x.type == type).spr;
        switch (type)
        {
            case TileType.Normal:
                //tileSpr.color = Color.white;
                break;
            case TileType.Bomb:
                //tileSpr.color = Color.red;
                break;
            case TileType.Bonus:
                //tileSpr.color = Color.yellow;
                break;
        }
    }
    public void UpdateTapCount()
    {
        tapCount++;
        gameObject.transform.DOScale(1.4f, 0.1f)
            .OnComplete(() => gameObject.transform.DOScale(1.2f,0.1f));
    }
    public void ShowRate(string rateStr)
    {
        rateText.enabled = true;
        rateText.text = $"x{rateStr}";
        rateText.transform.DOScale(2f, 1f);
        rateText.DOFade(0, 1f);
    }
    private void ResetRateText()
    {
        rateText.enabled = false;
        rateText.text = "";
        rateText.transform.localScale = Vector3.one;
        rateText.color = Color.white;
    }
    public void ShowItem()
    {
        tileSpr.enabled = false;
        itemSpr.enabled = true;
        if(type == TileType.Bonus) SoundManager.instance.PlaySE(SoundType.ShowTreasureBox);
    }
    public IEnumerator GetBonus(string bonusValueStr)
    {
        itemSpr.sprite = treasure;
        itemSpr.transform.DOScale(0.2f, 0.1f);
        yield return new WaitForSeconds(1f);
        itemSpr.enabled = false;
        ShowRate(bonusValueStr);
    }
    /// <summary>
    /// アイテム画像を非表示
    /// </summary>
    public void HideItem()
    {
        itemSpr.enabled = false;
    }

    /// <summary>
    /// 爆弾を爆発させる
    /// </summary>
    public void ExplodeBomb()
    {
        Debug.Log($"爆弾!!!!!!!!!!!!!!");
        itemSpr.transform.DOScale(0.15f, 0.05f).SetLoops(10, LoopType.Yoyo)
            .OnComplete(() =>
            {
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlaySE(SoundType.Explosion);
                itemSpr.sprite = explosionEffect;
                itemSpr.transform.DOScale(0.4f, 0.1f);
                redScreenFilter.enabled = true;
                redScreenFilter.transform.DOScale(200, 0.5f);
            });
    }
}
