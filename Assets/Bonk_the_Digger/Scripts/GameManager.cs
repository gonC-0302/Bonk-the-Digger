using UnityEngine;
public enum GameState
{
    Bet,            // 賭け金設定フェーズ
    SelectTile,     // タイル選択フェーズ
    BonusTap,
    MoveCharacter,  // キャラクター移動フェーズ
    Pause,
    Win,
    Lose
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private CashManager cashManager;
    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private CashOutPanel cashOutPanel;
    [SerializeField]
    private ResultCanvas resultPanel;
    [SerializeField]
    private AppCanvas appCanvas;
    private GameState currentState = GameState.Bet;
    public GameState CurrentState => currentState;
    
    /// <summary>
    /// 賭け金入力後、ゲームの準備フェーズ
    /// </summary>
    public void StartGame()
    {
        appCanvas.HideHeaderItems();
        tutorial.SetActive(true);
        currentState = GameState.SelectTile;
        cashOutPanel.ActivateCashOutButton();
    }
    /// <summary>
    /// タイル選択後、キャラクターの移動フェーズ
    /// </summary>
    public void ChangeToMoveCharacterState()
    {
        cashOutPanel.DeactivateCashOutButton();
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
        cashOutPanel.ActivateCashOutButton();
    }

    public void Win()
    {
        currentState = GameState.Win;
        cashOutPanel.CloseCashOutPanel();
        appCanvas.ShowHeaderItems();
        var rewardAmount = cashManager.CurrentCash;
        resultPanel.ActivateWinPanel(rewardAmount);
    }

    public void Lose()
    {
        currentState = GameState.Lose;
        cashOutPanel.CloseCashOutPanel();
        appCanvas.ShowHeaderItems();
        resultPanel.ActivateLosePanel();
    }

    public void PauseGame()
    {
        if (currentState != GameState.SelectTile) return;
        currentState = GameState.Pause;
    }
    public void ResumeGame()
    {
        if (currentState != GameState.Pause) return;
        currentState = GameState.SelectTile;
    }
}
