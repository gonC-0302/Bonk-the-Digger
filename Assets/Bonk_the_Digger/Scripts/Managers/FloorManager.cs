using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField]
    private Floor floorPrefab;
    [SerializeField]
    private Transform floorRoot;
    [SerializeField]
    private Transform characterTran;
    private int bombCount = 1;
    public int BombCount => bombCount;
    private List<Floor> floorsList = new List<Floor>();
    private float bottomFloorPosY;
    private int currentFloorNumber = 0;
    public int CurrentFloorNumber => currentFloorNumber;

    private void Start()
    {
        // 最初にフロア生成しておく
        SpawnFloors();
    }
    /// <summary>
    /// 最初に使用するフロアを全て生成しておく
    /// その後生成したフロアを使い回す
    /// </summary>
    private void SpawnFloors()
    {
        for (int i = 0; i < Constant.MAX_FLOOR_COUNT; i++)
        {
            var floor = Instantiate(floorPrefab, floorRoot);
            var offsetY = characterTran.position.y - 0.72f;
            var posY = i * Constant.FLOOR_GAP * -1 + offsetY;
            floor.transform.position = new Vector3(0, posY, 0);
            floor.SetFloorNumber(i);
            floorsList.Add(floor);
        }
        bottomFloorPosY = floorsList[Constant.MAX_FLOOR_COUNT - 1].transform.position.y;
        Debug.Log("フロア生成完了");
    }
    /// <summary>
    /// 最初に生成されたフロアに爆弾をセット
    /// </summary>
    /// <param name="floorNumber"></param>
    public void SetBombsInitialFloor(int bombCount)
    {
        if(bombCount < 1 || bombCount > 4)
        {
            throw new System.Exception("爆弾の個数で例外発生");
        }
        this.bombCount = bombCount;
        for (int i = 0; i < floorsList.Count; i++)
        {
            floorsList[i].SetTiles(bombCount);
        }
        Debug.Log("爆弾設置完了");

    }
    /// <summary>
    /// 現在の階層を更新する
    /// </summary>
    public void UpdateFloorNumber()
    {
        currentFloorNumber++;
        //Debug.Log($"現在の階層：{currentFloorNumber}");
        var outsideViewCount = 4;
        // 4フロア目以降になると、順次フロアを上に移動させて使い回す
        if (currentFloorNumber >= outsideViewCount) MoveFloor(currentFloorNumber - outsideViewCount);
    }
    /// <summary>
    /// カメラに映らなくなったフロアを初期化して使い回す
    /// </summary>
    /// <param name="targetFloorNumber"></param>
    public void MoveFloor(int targetFloorNumber)
    {
        var targetFloor = floorsList.Find(x => x.FloorNumber == targetFloorNumber);
        var targetPosY = bottomFloorPosY + Constant.FLOOR_GAP * -1;
        targetFloor.transform.position = new Vector3(0, targetPosY, 0);
        bottomFloorPosY = targetFloor.transform.position.y;
        targetFloor.SetFloorNumber(targetFloor.FloorNumber + Constant.MAX_FLOOR_COUNT);
        targetFloor.SetTiles(bombCount);
    }
}
