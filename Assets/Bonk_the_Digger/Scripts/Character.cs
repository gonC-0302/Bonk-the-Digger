using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private CashManager cashManager;
    [SerializeField]
    private Animator anim;

    /// <summary>
    /// 掘る対象のタイルの場所まで移動
    /// </summary>
    /// <param name="targetPosX"></param>
    public void MoveTargetTile(Tile tile)
    {
        gameManager.ChangeToMoveCharacterState();
        var diff = gameObject.transform.position.x - tile.transform.position.x;
        if(Mathf.Abs(diff) <= 0.1f)
        {
            StartCoroutine(PlayDigAnimation(tile));
        }
        else
        {
            ChangeDirection(diff);
            var targetPosX = tile.transform.position.x;
            anim.SetBool("IsRunning", true);
            var moveTime = Mathf.Abs(diff) / 0.775f * 0.5f;
            gameObject.transform.DOMoveX(targetPosX, moveTime).SetEase(Ease.Linear).
                OnComplete(() =>
                {
                    anim.SetBool("IsRunning", false);
                    StartCoroutine(PlayDigAnimation(tile));
                });
        }
    }
    public void PlayMoveSE()
    {
        if (gameManager.CurrentPhase != GamePhase.MoveCharacter) return;
        SoundManager.instance.PlaySE(SoundType.Move);
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
        anim.SetBool("IsDigging", true);
        tile.PlayDigTileAnimation();
        yield return new WaitForSeconds(1f);
        switch (tile.Type)
        {
            case TileType.Normal:
                tile.SpawnCoin();
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("IsDigging", false);
                GetCoin();
                yield break;
            case TileType.Bomb:
                gameManager.Lose();
                tile.SpawnBomb();
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("IsDigging", false);
                anim.SetTrigger("Panic");
                yield return new WaitForSeconds(1.5f);
                gameManager.ActivateLosePanel();
                yield return new WaitForSeconds(0.75f);
                transform.position = new Vector3(0, transform.position.y);
                SoundManager.instance.PlaySE(SoundType.GameOver);
                anim.Play("Lose");
                yield break;
            case TileType.ChallengeBox:
                var bonusRate = cashManager.CalculateBonusRate();
                tile.SpawnChallengeBox(bonusRate);
                gameManager.StartBonusTap();
                yield break;
        }
    }

    public void GetCoin()
    {
        cashManager.EvaluateCurrentCashBackAmount();
        PlayJumpAnimation();
    }
    public IEnumerator GetTreasureBox(Tile tile)
    {
        anim.SetBool("IsDigging", false);
        //var bonusRate = cashManager.CalculateBonusRate();
        //var rateStr = bonusRate.ToString();
        //tile.PlayBonusAnimation(rateStr);
        var rate = tile.GetBonusRate();
        PlayJumpAnimation();
        yield return new WaitForSeconds(1.5f);
        cashManager.GetBonus(rate);
    }
    public void PlayJumpAnimation()
    {
        anim.SetTrigger("Jump");
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
