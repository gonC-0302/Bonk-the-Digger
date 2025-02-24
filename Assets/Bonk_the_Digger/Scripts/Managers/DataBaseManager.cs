using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;
    public SoundDataSO soundDataSO;
    public TileItemDataSO tileDataSO;

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
