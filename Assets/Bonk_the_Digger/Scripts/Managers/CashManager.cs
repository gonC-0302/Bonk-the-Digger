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
    private CashBackRateDataSO dataSO;

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
    public void EvaluateCurrentCashBackAmount(int clearCount)
    {
        if (clearCount < 1) return;
        currentRate = CalclateCashBackRate(clearCount);
        float resultCashFloat = betAmount * currentRate;
        cashBackAmount = Mathf.FloorToInt(resultCashFloat);
        UpdateCashBackAmountText();
    }
    private float CalclateCashBackRate(int clearCount)
    {
        var data = dataSO.ratesList.Find(x => x.bombCount == floorManager.BombCount);
        float cashBackRate = this.currentRate;
        if (clearCount == 1) cashBackRate *= data.baseRate;
        else cashBackRate *= data.multipleRate;
        // 賞金倍率は小数第二位までで計算（小数第三位を四捨五入）
        var cashBackRateStr = cashBackRate.ToString("f2");
        cashBackRate = float.Parse(cashBackRateStr);
        return cashBackRate;
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
        currentRateText.text = $"x{currentRate.ToString()}";
    }
}
