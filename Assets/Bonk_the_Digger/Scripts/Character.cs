using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private SoundManager soundManager;

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
        gameObject.transform.DOMoveX(targetPosX, 0.1f).
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
        yield return new WaitForSeconds(0.1f);
        tile.OpenTile();
        // TODO: タイルに爆弾がないか確認して進む（爆弾あれば終了）
        switch (tile.Type)
        {
            case TileType.Normal:
                yield return new WaitForSeconds(0.1f);
                tile.GetItem();
                GoNextFloor();
                yield break;
            case TileType.Bomb:
                Debug.Log($"爆弾!!!!!!!!!!!!!!");
                tile.ExplodeBomb();
                yield return new WaitForSeconds(0.5f);
                soundManager.PlayBombSE();
                yield break;
            case TileType.Bonus:
                gameManager.StartBonusTap();
                yield break;
        }
    }
    /// <summary>
    /// 次のフロアに降りる
    /// </summary>
    public void GoNextFloor()
    {
        soundManager.PlayGetMeatSE();
        var targetPosY = transform.position.y + Constant.FLOOR_GAP * -1;
        gameObject.transform.DOMoveY(targetPosY, 0.1f).
            OnComplete(() => gameManager.ArriveAtNextFloor());
    }
}
