using UnityEngine;
using System.Collections;

public enum GamePhase
{
    Bet,            // 賭け金設定フェーズ
    SelectTile,     // タイル選択フェーズ
    BonusTap,       // ボーナスタップフェーズ
    MoveCharacter,  // キャラクター移動フェーズ
    Pause,
    Win,
    Lose
}

/// <summary>
/// ゲームフェーズを管理
/// </summary>
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
        floorManager.UpdateFloorNumber();
        currentPhase = GamePhase.SelectTile;
        cashOutPanel.ActivateCashOutButton();
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
    public void Win()
    {
        currentPhase = GamePhase.Win;
        cashOutPanel.DisableCashOutPanel();
        appCanvas.ShowHeaderInfomations();
        var cashBackAmount = Mathf.Clamp(cashManager.CashBackAmount,0,999999999);
        resultPanel.ActivateWinPanel(cashBackAmount);
    }
    public void Lose()
    {
        currentPhase = GamePhase.Lose;
      
    }
    public void ActivateLosePanel()
    {
        cashOutPanel.DisableCashOutPanel();
        appCanvas.ShowHeaderInfomations();
        resultPanel.ActivateLosePanel();
    }
}
