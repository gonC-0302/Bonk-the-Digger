using UnityEngine;

public enum SoundType
{
    SelectBombCount,
    Dig,
    GetMeat,
    ShowTreasureBox,
    TapTreasureBox,
    Explosion
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    public static SoundManager instance;

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
    public void StopBGM()
    {
        source.Stop();
    }

    public void PlaySE(SoundType type)
    {
        var clip = DataBaseManager.instance.soundDataSO.soundDatasList.Find(x => x.type == type).clip;
        source.PlayOneShot(clip);
    }
}
