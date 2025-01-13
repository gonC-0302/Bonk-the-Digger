using UnityEngine;

public enum SoundType
{
    SelectBombCount,    // 爆弾の数選択SE
    Move,
    Dig,                // 掘るSE
    GetCoin,            // コイン獲得SE
    ShowChallengeBox,   // チャレンジボックス出現SE
    TapChallengeBox,    // チャレンジボックスタップSE
    Explosion,          // 爆発SE
    GameOver,
    ClickButton,
    CashOutButton,
    ChallengeBox_Appear,
    ChallengeBox_Hit,
    ChallengeBox_Open,
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
