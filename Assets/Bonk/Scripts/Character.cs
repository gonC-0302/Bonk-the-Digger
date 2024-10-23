using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;

    /// <summary>
    /// 掘る対象のタイルの場所まで移動
    /// </summary>
    /// <param name="targetPosX"></param>
    public void MoveTargetTile(Tile tile)
    {
        manager.ChangeStateToAnimation();
        var targetPosX = tile.transform.position.x;
        gameObject.transform.DOMoveX(targetPosX, 0.1f).
            OnComplete(() => StartCoroutine(PlayDigAnimation(tile)));
    }
    /// <summary>
    /// 掘るアニメーションを開始
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    private IEnumerator PlayDigAnimation(Tile tile)
    {
        yield return new WaitForSeconds(0.1f);
        tile.Deactivate();
        GoNextFloor();
    }
    /// <summary>
    /// 次のフロアに降りる
    /// </summary>
    private void GoNextFloor()
    {
        var targetPosY = transform.position.y + Constant.FLOOR_GAP * -1;
        gameObject.transform.DOMoveY(targetPosY, 0.1f).
            OnComplete(() => manager.ChangeStateToPreparate());
    }
}
