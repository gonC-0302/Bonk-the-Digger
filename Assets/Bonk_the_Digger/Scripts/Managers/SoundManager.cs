using UnityEngine;

public enum SoundType
{
    SelectBombCount,    // 爆弾の数選択SE
    Dig,                // 掘るSE
    GetCoin,            // コイン獲得SE
    ShowChallengeBox,   // チャレンジボックス出現SE
    TapChallengeBox,    // チャレンジボックスタップSE
    Explosion,          // 爆発SE
    GameOver
      
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
        }
        else
        {
            Destroy(gameObject);
        }
        source.PlayDelayed(0.000001f);
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
