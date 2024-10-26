using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip bombSE,meatSE,tapTreasureBoxSE;

    public void PlayBombSE()
    {
        source.Stop();
        source.PlayOneShot(bombSE);
    }

    public void PlayGetMeatSE()
    {
        source.PlayOneShot(meatSE);
    }

    public void PlayTapTreasureBoxSE()
    {
        source.PlayOneShot(tapTreasureBoxSE);
    }
}
