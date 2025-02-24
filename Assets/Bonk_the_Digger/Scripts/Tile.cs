using NUnit.Framework.Interfaces;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using UnityEditor;

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
    [SerializeField]
    private SpriteRenderer hitEffectPrefab;
    [SerializeField]
    private Sprite silverBonus, goldBonus, rainbowBonus;
    private SpriteRenderer hitEffect;
    private Animator challengeBoxAnim;
    private SpriteRenderer challengeBoxSpr;
    private float bonusRate;
    private int floorNumber;
    public int FloorNumber => floorNumber;
    private TileType type;
    public TileType Type => type;
    private int tapCount;
    public int TapCount => tapCount;
    private bool isOpendChallengeBox;
    public bool IsOpenedChallengeBox => isOpendChallengeBox;

    public void InitTile(int floorNumber,TileType type)
    {
        this.floorNumber = floorNumber;
        this.type = type;
        isOpendChallengeBox = false;
        tapCount = 0;
        bonusRate = 1;
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
        hitEffect.color = new Color(1,1,1, tapCount * 0.1f);
        if (tapCount == 1)
        {
            hitEffect.GetComponent<Animator>().SetTrigger("Hit");
            SoundManager.instance.PlaySE(SoundType.ChallengeBox_Hit);
        }
    }
    public IEnumerator ShowRate(string rateStr)
    {
        rateText.enabled = true;
        rateText.text = $"x{rateStr}";
        yield return new WaitForSeconds(1f);
        rateText.DOFade(0, 1f);
    }
    private void ResetRateText()
    {
        rateText.enabled = false;
        rateText.text = "";
        rateText.transform.localScale = Vector3.one;
        rateText.color = Color.white;
    }
    public void PlayBonusAnimation(string bonusValueStr)
    {
        HideChallengeBoxGuide();
        Destroy(hitEffect);
        challengeBoxAnim.enabled = true;
        challengeBoxAnim.SetTrigger("Open");
        StartCoroutine(SpawnBonusItem(bonusValueStr));
    }
    private IEnumerator SpawnBonusItem(string bonusValueStr)
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(ShowRate(bonusValueStr));
        if (challengeBox == null) yield break;
        Destroy(challengeBox,1f);
        //Destroy(bonusItem);
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
    public void SpawnChallengeBox(float rate)
    {
        challengeBox = Instantiate(challengeBoxPrefab, transform);
        challengeBoxAnim = challengeBox.GetComponent<Animator>();
        challengeBoxSpr = challengeBox.GetComponent<SpriteRenderer>();
        this.bonusRate = rate;
        if(rate == 100)
        {
            challengeBoxSpr.sprite = rainbowBonus;
        }
        else if(rate == 30 || rate == 5)
        {
            challengeBoxSpr.sprite = goldBonus;

        }
        else if(rate == 1 || rate == 0.1f)
        {
            challengeBoxSpr.sprite = silverBonus;
        }
        hitEffect = Instantiate(hitEffectPrefab,challengeBox.transform);
        SoundManager.instance.PlaySE(SoundType.ChallengeBox_Appear);
    }
    private void HideChallengeBoxGuide()
    {
        if (challengeBox == null) return;
        challengeBox.transform.GetChild(0).gameObject.SetActive(false);
    }
    public float GetBonusRate()
    {
        return bonusRate;
    }
    public void OpenChallengeBox()
    {
        isOpendChallengeBox = true;
        var rateStr = bonusRate.ToString();
        PlayBonusAnimation(rateStr);
    }
}
