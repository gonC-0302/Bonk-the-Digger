using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Preparate,
    SelectTile,
    Animation,
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private Character character;
    private GameState currentState = GameState.Preparate;
    public GameState CurrentState => currentState;
    private int currentFloorNumber = 0;
    public int CurrentFloorNumber => currentFloorNumber;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        currentState = GameState.Preparate;
        // 最初に4フロア生成しておく
        floorManager.PreparateInitFloors();
        // タイル選択フェーズに移行
        currentState = GameState.SelectTile;
    }

    /// <summary>
    /// タイルを選択した後
    /// キャラクターアニメーションフェーズに遷移
    /// </summary>
    public void ChangeStateToAnimation()
    {
        currentState = GameState.Animation;
    }
    /// <summary>
    /// キャラクターが下のフロアに到着後
    /// </summary>
    public void ChangeStateToPreparate()
    {
        currentState = GameState.Preparate;
        currentFloorNumber++;
        //Debug.Log($"現在の階層{currentFloorNumber}");
        // 現在の階層の3つ上はカメラに映らなくなる
        var outsideViewCount = 3;
        if(currentFloorNumber >= outsideViewCount)
        {
            floorManager.MoveFloor(currentFloorNumber - outsideViewCount);
        }
        ChangeStateToSelectTile();
    }
    /// <summary>
    /// フロアの描画など準備を終えた後
    /// </summary>
    private void ChangeStateToSelectTile()
    {
        currentState = GameState.SelectTile;
        Debug.Log($"タイルを選択して下さい");
    }
}
