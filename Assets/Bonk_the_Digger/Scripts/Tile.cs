using NUnit.Framework.Interfaces;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public enum TileType
{
    Normal,
    Bomb,
    ChallengeBox,
}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer tileSpriteRender;
    [SerializeField]
    private Sprite tileSpr, tileSpr_digged;
    [SerializeField]
    private TextMeshPro rateText;
    [SerializeField]
    private GameObject bombPrefab,challengeBoxPrefab;
    private GameObject challengeBox;
    [SerializeField]
    private GameObject smokePrefab;
    [SerializeField]
    private GameObject jewerlyPrefab,bonePrefab, coinPrefab;
    [SerializeField]
    private Animator anim;
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
        anim.Play("Idle");
        switch (type)
        {
            case TileType.Normal:
                tileSpriteRender.color = Color.white;
                break;
            case TileType.Bomb:
                tileSpriteRender.color = Color.red;
                break;
            case TileType.ChallengeBox:
                tileSpriteRender.color = Color.green;
                break;
        }
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
    public void PlayDigTileAnimation()
    {
        anim.SetTrigger("Dig");
    }
    public void SpawnCoin()
    {
        var coin =  Instantiate(coinPrefab, transform);
        SoundManager.instance.PlaySE(SoundType.GetCoin);
        Destroy(coin, 2f);
    }
    public void SpawnBomb()
    {
        Instantiate(bombPrefab, transform);
        SoundManager.instance.PlaySE(SoundType.Explosion);
    }
    public void SpawnChallengeBox()
    {
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
