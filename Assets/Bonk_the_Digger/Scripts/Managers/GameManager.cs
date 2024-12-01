using UnityEngine;
public enum GamePhase
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
    private GamePhase currentPhase = GamePhase.Bet;
    public GamePhase CurrentPhase => currentPhase;
    
    /// <summary>
    /// 賭け金入力後、ゲームの準備フェーズ
    /// </summary>
    public void StartGame()
    {
        appCanvas.HideHeaderInfomations();
        tutorial.SetActive(true);
        currentPhase = GamePhase.SelectTile;
        cashOutPanel.ActivateCashOutButton();
    }
    /// <summary>
    /// タイル選択後、キャラクターの移動フェーズ
    /// </summary>
    public void ChangeToMoveCharacterState()
    {
        cashOutPanel.DeactivateCashOutButton();
        currentPhase = GamePhase.MoveCharacter;
    }
    public void StartBonusTap()
    {
        currentPhase = GamePhase.BonusTap;
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
        currentPhase = GamePhase.SelectTile;
        cashOutPanel.ActivateCashOutButton();
    }

    public void Win()
    {
        currentPhase = GamePhase.Win;
        cashOutPanel.CloseCashOutPanel();
        appCanvas.ShowHeaderInfomations();
        var rewardAmount = Mathf.Clamp(cashManager.CurrentCash,0,9999999);
        resultPanel.ActivateWinPanel(rewardAmount);
    }

    public void Lose()
    {
        currentPhase = GamePhase.Lose;
        cashOutPanel.CloseCashOutPanel();
        appCanvas.ShowHeaderInfomations();
        resultPanel.ActivateLosePanel();
    }

    public void PauseGame()
    {
        if (currentPhase != GamePhase.SelectTile) return;
        currentPhase = GamePhase.Pause;
    }
    public void ResumeGame()
    {
        if (currentPhase != GamePhase.Pause) return;
        currentPhase = GamePhase.SelectTile;
    }
}
