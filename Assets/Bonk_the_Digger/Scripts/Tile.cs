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
    [SerializeField]
    private GameObject coinPrefab,bombPrefab,challengeBoxPrefab;
    private GameObject challengeBox;
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
        tileSpr.enabled = true;
        //switch (type)
        //{
        //    case TileType.Normal:
        //        tileSpr.color = Color.white;
        //        break;
        //    case TileType.Bomb:
        //        tileSpr.color = Color.red;
        //        break;
        //    case TileType.Bonus:
        //        tileSpr.color = Color.green;

        //        break;
        //}
    }
    public void UpdateTapCount()
    {
        tapCount++;
        if (tapCount == 1) HideChallengeBoxGuide();
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
    //public void ShowItem()
    //{
    //    tileSpr.enabled = false;
    //    switch (type)
    //    {
    //        case TileType.Normal:
    //            SpawnCoin();
    //            break;
    //        case TileType.Bomb:
    //            SpawnBomb();
    //            break;
    //        case TileType.Bonus:
    //            //tileSpr.color = Color.yellow;
    //            break;
    //    }
    //    if (type == TileType.Bonus) SoundManager.instance.PlaySE(SoundType.ShowTreasureBox);
    //}
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
    //public void HideItem()
    //{
    //    itemSpr.enabled = false;
    //}

    public void SpawnCoin()
    {
        tileSpr.enabled = false;
        var coin =  Instantiate(coinPrefab, transform);
        Destroy(coin, 2f);
    }

    public IEnumerator SpawnBomb()
    {
        tileSpr.enabled = false;
        Instantiate(bombPrefab, transform);
        yield return new WaitForSeconds(2f);
        //itemSpr.sprite = explosionEffect;
        //itemSpr.transform.DOScale(0.4f, 0.1f);
        //redScreenFilter.enabled = true;
        //redScreenFilter.transform.DOScale(200, 0.5f);
    }

    public void SpawnChallengeBox()
    {
        tileSpr.enabled = false;
        challengeBox = Instantiate(challengeBoxPrefab, transform);
    }
    private void HideChallengeBoxGuide()
    {
        if (challengeBox == null) return;
        challengeBox.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void HideChallengeBox()
    {
        if (challengeBox == null) return;
        Destroy(challengeBox);
    }

    /// <summary>
    /// 爆弾を爆発させる
    /// </summary>
    //public void ExplodeBomb()
    //{
    //    Debug.Log($"爆弾!!!!!!!!!!!!!!");
    //    itemSpr.transform.DOScale(0.15f, 0.05f).SetLoops(10, LoopType.Yoyo)
    //        .OnComplete(() =>
    //        {
    //            SoundManager.instance.StopBGM();
    //            SoundManager.instance.PlaySE(SoundType.Explosion);
    //            //itemSpr.sprite = explosionEffect;
    //            //itemSpr.transform.DOScale(0.4f, 0.1f);
    //            //redScreenFilter.enabled = true;
    //            //redScreenFilter.transform.DOScale(200, 0.5f);
    //        });
    //}
}
