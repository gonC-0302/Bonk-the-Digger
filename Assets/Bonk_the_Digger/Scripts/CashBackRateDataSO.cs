using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CashBackRateDateSO", fileName = "CashBackRateDate")]
public class CashBackRateDataSO : ScriptableObject
{
    public List<Rate> ratesList = new List<Rate>();

    [Serializable]
    public class Rate
    {
        public int bombCount;
        public float baseRate;
        public float multipleRate;
    }
}
