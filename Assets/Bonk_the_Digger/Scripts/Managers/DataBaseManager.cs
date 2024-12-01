using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    public SoundDataSO soundDataSO;
    public TileItemDataSO tileDataSO;
    public CashBackRateDataSO cashBackRateDataSO;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
