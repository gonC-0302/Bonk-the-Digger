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

    public void SetBet(int betValue)
    {
        this.betValue = betValue;
        Debug.Log($"賭け金を{betValue}で確定");
        currentCash = betValue;
        UpdateCashText();
    }
    /// <summary>
    /// 階層を進めることによるキャッシュ増加
    /// </summary>
    public void IncreaseCashByNextFloor(int floorNumber)
    {
        var data = dataSO.ratesList.Find(x => x.bombCount == floorManager.BombCount);
        EvaluateCurrentCash(data.baseRate, data.multipleRate, floorNumber);
    }
    /// <summary>
    /// 現在のキャッシュ額を計算して表示更新
    /// </summary>
    /// <param name="baseRate"></param>
    /// <param name="multipleRate"></param>
    /// <param name="floorNumber"></param>
    private void EvaluateCurrentCash(float baseRate, float multipleRate, int floorNumber)
    {
        if (floorNumber < 1) return;

        float resultCashFloat;
        var initialFloorNumber = 1;
        // 2階層の時(1回クリア)
        if(floorNumber == initialFloorNumber + 1)
        {
            resultCashFloat = betValue * baseRate;
            Debug.Log($"賞金倍率：{baseRate}(基本倍率)");
            Debug.Log($"現在の払い戻し金額：{betValue} * {baseRate} = {betValue * baseRate}");
        }
        // 3階層以降(2回クリア以降)
        else
        {
            float rate = Mathf.Pow(multipleRate, floorNumber - 2);
            // 賞金倍率は小数第二位までで計算（小数第三位を四捨五入）
            var rateStr = rate.ToString("f2");
            Debug.Log($"賞金倍率：{baseRate * float.Parse(rateStr)}");
            resultCashFloat = betValue * baseRate * float.Parse(rateStr);
            Debug.Log($"現在の払い戻し金額：{betValue} * {baseRate} * {float.Parse(rateStr)} = {betValue * baseRate * float.Parse(rateStr)}");
        }
        currentCash = Mathf.FloorToInt(resultCashFloat);
        UpdateCashText();
    }
    private void UpdateCashText()
    {
        cashText.text = $"${currentCash.ToString()}";
    }
}
