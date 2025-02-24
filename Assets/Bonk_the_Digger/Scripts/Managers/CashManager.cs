using TMPro;
using UnityEngine;

/// <summary>
/// 賭け金・レート・払い戻し額の管理
/// </summary>
public class CashManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentRateText;        // 現在のレートテキスト
    private float currentRate;                      //現在のレート
    [SerializeField]
    private TextMeshProUGUI betAmountText;          // 賭け金テキスト
    private int betAmount;                          //賭け金
    private int cashBackAmount;                     // 払い戻し額
    public int CashBackAmount => cashBackAmount;
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private BonusRateDataSO dataSO;

    /// <summary>
    /// 初期の賭け金を登録
    /// </summary>
    /// <param name="betValue"></param>
    public void SetBet(int betValue)
    {
        this.betAmount = betValue;
        betAmountText.text = betValue.ToString();
        currentRate = 1;
        Debug.Log($"賭け金を{betValue}で確定");
        cashBackAmount = betValue;
        UpdateCashBackAmountText();
    }
    public string GetCurrentRate()
    {
        return currentRate.ToString("f2");
    }
    /// <summary>
    /// 現在のキャッシュバック額を計算して表示更新
    /// </summary>
    /// <param name="baseRate"></param>
    /// <param name="multipleRate"></param>
    /// <param name="floorNumber"></param>
    public void EvaluateCurrentCashBackAmount()
    {
        float rate = CalculateCashBackRate();
        currentRate *= rate;
        float resultCashFloat = betAmount * currentRate;
        cashBackAmount = Mathf.FloorToInt(resultCashFloat);
        UpdateCashBackAmountText();
    }
    private float CalculateCashBackRate()
    {
        int maxTileCount = 5;
        float bombCount = floorManager.BombCount;
        float successRate = (maxTileCount - bombCount) / maxTileCount;
        return 1 / successRate;
    }
    /// <summary>
    /// チャレンジボックスのボーナス
    /// </summary>
    /// <param name="bonusMultiplier"></param>
    public void GetBonus(float bonusMultiplier)
    {
        currentRate *= bonusMultiplier;
        UpdateCashBackAmountText();
    }
    private void UpdateCashBackAmountText()
    {
        var rateStr = currentRate.ToString("f2");
        currentRateText.text = $"x{float.Parse(rateStr).ToString()}";
    }
    public float CalculateBonusRate()
    {
        var list = dataSO.ratesList.Find(x => x.bombCount == floorManager.BombCount);
        float random = Random.Range(0, 1000);
        if(random < list.probabilitiesList[4])
        {
            return 100;
        }
        else if (random < list.probabilitiesList[4]+list.probabilitiesList[3])
        {
            return 30;
        }
        else if (random < list.probabilitiesList[4] + list.probabilitiesList[3]+list.probabilitiesList[2])
        {
            return 5;
        }
        else if (random < list.probabilitiesList[4] + list.probabilitiesList[3] + list.probabilitiesList[2] + list.probabilitiesList[1])
        {
            return 1;
        }
        else if (random < list.probabilitiesList[4] + list.probabilitiesList[3] + list.probabilitiesList[2] + list.probabilitiesList[1] + list.probabilitiesList[0])
        {
            return 0.1f;
        }
        else
        {
            Debug.LogError("不正な値");
            return 0;
        }
    }
}
