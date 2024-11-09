using System;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cashText;
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private CashBackRateDataSO dataSO;
    private int currentCash;
    private int betValue;
    private float currentRate;

    /// <summary>
    /// 初期の賭け金を登録
    /// </summary>
    /// <param name="betValue"></param>
    public void SetBet(int betValue)
    {
        this.betValue = betValue;
        currentRate = 1;
        Debug.Log($"賭け金を{betValue}で確定");
        currentCash = betValue;
        UpdatePrizeMultiplierText();
    }
    public string GetCurrentRate()
    {
        return currentRate.ToString("f2");
    }
    /// <summary>
    /// 現在のキャッシュ額を計算して表示更新
    /// </summary>
    /// <param name="baseRate"></param>
    /// <param name="multipleRate"></param>
    /// <param name="floorNumber"></param>
    public void EvaluateCurrentCash(int clearCount)
    {
        if (clearCount < 1) return;
        currentRate = CalclateCashBackRate(clearCount);
        float resultCashFloat = betValue * currentRate;
        currentCash = Mathf.FloorToInt(resultCashFloat);
        UpdatePrizeMultiplierText();
    }
    //public float CalclateCashBackRate(int clearCount)
    //{
    //    var data = dataSO.ratesList.Find(x => x.bombCount == floorManager.BombCount);

    //    float cashBackRate;
    //    if (clearCount == 1)
    //    {
    //        cashBackRate = data.baseRate;
    //    }
    //    else
    //    {
    //        float rate = Mathf.Pow(data.multipleRate, clearCount - 1);
    //        // 賞金倍率は小数第二位までで計算（小数第三位を四捨五入）
    //        var cashBackRateStr = (data.baseRate * rate).ToString("f2");
    //        cashBackRate = float.Parse(cashBackRateStr);
    //    }
    //    Debug.Log($"賞金倍率：{cashBackRate}");
    //    return cashBackRate;
    //}
    //private void UpdateCashText()
    //{
    //    cashText.text = $"${currentCash.ToString()}";
    //}
    public void GetBonus(float bonusMultiplier)
    {
        currentRate *= bonusMultiplier;
        UpdatePrizeMultiplierText();
    }
    private void UpdatePrizeMultiplierText()
    {
        cashText.text = $"x{currentRate.ToString()}";
    }
    /// <summary>
    /// 現在のキャッシュ額を計算して表示更新
    /// </summary>
    /// <param name="baseRate"></param>
    /// <param name="multipleRate"></param>
    /// <param name="floorNumber"></param>
    //public void EvaluateCurrentCash(int clearCount)
    //{
    //    if (clearCount < 1) return;
    //    currentRate = CalclateCashBackRate(clearCount);
    //    Debug.Log($"賞金倍率：{currentRate}");
    //    float resultCashFloat = betValue * currentRate;
    //    currentCash = Mathf.FloorToInt(resultCashFloat);
    //    UpdateCashText();
    //}
    private float CalclateCashBackRate(int clearCount)
    {
        var data = dataSO.ratesList.Find(x => x.bombCount == floorManager.BombCount);
        float cashBackRate = this.currentRate;
        if (clearCount == 1)
        {
            cashBackRate *= data.baseRate;
        }
        else
        {
            cashBackRate *= data.multipleRate;
        }
        // 賞金倍率は小数第二位までで計算（小数第三位を四捨五入）
        var cashBackRateStr = cashBackRate.ToString("f2");
        cashBackRate = float.Parse(cashBackRateStr);
        return cashBackRate;
    }
}
