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
    private SpriteRenderer tileSpriteRender;
    [SerializeField]
    private Sprite tileSpr, tileSpr_digged;
    [SerializeField]
    private Sprite explosionEffect,treasure;
    [SerializeField]
    private SpriteRenderer redScreenFilter;
    [SerializeField]
    private TextMeshPro rateText;
    [SerializeField]
    private GameObject coinPrefab,bombPrefab,challengeBoxPrefab;
    private GameObject challengeBox;
    [SerializeField]
    private GameObject smokePrefab;
    [SerializeField]
    private GameObject jewerlyPrefab,bonePrefab;
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
        tileSpriteRender.sprite = tileSpr;
        tileSpriteRender.enabled = true;
        //switch (type)
        //{
        //    case TileType.Normal:
        //        tileSpriteRender.color = Color.white;
        //        break;
        //    case TileType.Bomb:
        //        tileSpriteRender.color = Color.red;
        //        break;
        //    case TileType.Bonus:
        //        tileSpriteRender.color = Color.green;
        //        break;
        //}
    }
    public void UpdateTapCount()
    {
        tapCount++;
        if (tapCount == 1) HideChallengeBoxGuide();
        challengeBox.transform.DOScale(1.2f, 0.1f)
            .OnComplete(() => challengeBox.transform.DOScale(1.0f,0.1f).SetLink(challengeBox)).SetLink(challengeBox);
    }
    public IEnumerator ShowRate(string rateStr)
    {
        rateText.enabled = true;
        rateText.text = $"x{rateStr}";
        rateText.transform.DOScale(2f, 0.25f).OnComplete(() => rateText.transform.DOScale(1f, 0.25f));
        yield return new WaitForSeconds(2f);
        rateText.DOFade(0, 1f);
    }
    private void ResetRateText()
    {
        rateText.enabled = false;
        rateText.text = "";
        rateText.transform.localScale = Vector3.one;
        rateText.color = Color.white;
    }
    public IEnumerator PlayBonusAnimation(string bonusValueStr)
    {
        var smoke = Instantiate(smokePrefab, transform);
        yield return new WaitForSeconds(1f);
        GameObject bonusItem;
        float bonusValue = float.Parse(bonusValueStr);
        if(bonusValue >= 1)
        {
            bonusItem = Instantiate(jewerlyPrefab, transform);
            rateText.color = Color.yellow;
        }
        else
        {
            bonusItem = Instantiate(bonePrefab, transform);
            rateText.color = Color.gray;

        }
        yield return new WaitForSeconds(2f);
        Destroy(smoke);
        Destroy(bonusItem);
        StartCoroutine(ShowRate(bonusValueStr));
    }
    public void SpawnCoin()
    {
        tileSpriteRender.sprite = tileSpr_digged;
        var coin =  Instantiate(coinPrefab, transform);
        Destroy(coin, 2f);
    }
    public IEnumerator SpawnBomb()
    {
        tileSpriteRender.sprite = tileSpr_digged;
        Instantiate(bombPrefab, transform);
        yield return new WaitForSeconds(2f);
    }
    public void SpawnChallengeBox()
    {
        tileSpriteRender.sprite = tileSpr_digged;
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
}
