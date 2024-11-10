using System.Collections;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private CashManager cashManager;
    [SerializeField]
    private Animator anim;
    private int clearCount;

    /// <summary>
    /// 掘る対象のタイルの場所まで移動
    /// </summary>
    /// <param name="targetPosX"></param>
    public void MoveTargetTile(Tile tile)
    {
        gameManager.ChangeToMoveCharacterState();
        var diff = gameObject.transform.position.x - tile.transform.position.x;
        ChangeDirection(diff);
        var targetPosX = tile.transform.position.x;
        gameObject.transform.DOMoveX(targetPosX, 0.25f).
            OnComplete(() => StartCoroutine(PlayDigAnimation(tile)));
    }
    private void ChangeDirection(float diff)
    {
        // 左
        if (diff > 0) transform.localScale = new Vector3(transform.localScale.y * -1, transform.localScale.y);
        // 右
        else if (diff < 0) transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y);
    }
    /// <summary>
    /// 掘るアニメーションを開始
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private IEnumerator PlayDigAnimation(Tile tile)
    {
        SoundManager.instance.PlaySE(SoundType.Dig);
        // TODO: 掘るアニメーション
        yield return new WaitForSeconds(1f);
        //tile.ShowItem();
        switch (tile.Type)
        {
            case TileType.Normal:
                tile.SpawnCoin();
                yield return new WaitForSeconds(0.75f);
                GetItem(tile);
                yield break;
            case TileType.Bomb:
                yield return StartCoroutine(tile.SpawnBomb());
                gameManager.Lose();
                anim.Play("Lose");
                //tile.ExplodeBomb();
                yield break;
            case TileType.Bonus:
                tile.SpawnChallengeBox();
                gameManager.StartBonusTap();
                yield break;
        }
    }

    public void GetItem(Tile tile)
    {
        clearCount++;
        cashManager.EvaluateCurrentCash(clearCount);
        SoundManager.instance.PlaySE(SoundType.GetMeat);
        GoNextFloor();
    }
    public IEnumerator GetTreasureBox(Tile tile)
    {
        clearCount++;
        float random = Random.Range(0.5f, 4f);
        string randomStr = random.ToString("f1");
        cashManager.GetBonus(float.Parse(randomStr));
        yield return StartCoroutine(tile.GetBonus(randomStr));
        //cashManager.EvaluateCurrentCash(clearCount);
        SoundManager.instance.PlaySE(SoundType.GetMeat);
        GoNextFloor();
    }
    /// <summary>
    /// 次のフロアに降りる
    /// </summary>
    public void GoNextFloor()
    {
        var targetPosY = transform.position.y + Constant.FLOOR_GAP * -1;
        gameObject.transform.DOMoveY(targetPosY, 0.25f).
            OnComplete(() => gameManager.ArriveAtNextFloor());
    }

    public void PlayWinAnimation()
    {
        anim.Play("Win");
    }
}
