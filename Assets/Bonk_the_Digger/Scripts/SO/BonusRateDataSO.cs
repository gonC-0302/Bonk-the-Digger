using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BonusRateDataSO", fileName = "Scriptable Objects/BonusRateData")]
public class BonusRateDataSO : ScriptableObject
{
    public List<Rate> ratesList = new List<Rate>();

    [Serializable]
    public class Rate
    {
        public int bombCount;
        public List<float> probabilitiesList;
    }
}
