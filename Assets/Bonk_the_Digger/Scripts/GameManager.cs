using UnityEngine;
public enum GameState
{
    Bet,            // 賭け金設定フェーズ
    SelectTile,     // タイル選択フェーズ
    BonusTap,
    MoveCharacter,  // キャラクター移動フェーズ
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private CashManager cashManager;
    private GameState currentState = GameState.Bet;
    public GameState CurrentState => currentState;

    /// <summary>
    /// 賭け金入力後、ゲームの準備フェーズ
    /// </summary>
    public void StartGame()
    {
        currentState = GameState.SelectTile;
    }
    /// <summary>
    /// タイル選択後、キャラクターの移動フェーズ
    /// </summary>
    public void ChangeToMoveCharacterState()
    {
        currentState = GameState.MoveCharacter;
    }
    public void StartBonusTap()
    {
        currentState = GameState.BonusTap;
    }
    /// <summary>
    /// キャラクター移動完了後、タイル選択フェーズ
    /// </summary>
    public void ArriveAtNextFloor()
    {
        // 現在のフロアNo.を更新
        floorManager.UpdateFloorNumber();
        // 現在のキャッシュを増加
        //cashManager.IncreaseCashBack(floorManager.CurrentFloorNumber);
        currentState = GameState.SelectTile;
    }
}
