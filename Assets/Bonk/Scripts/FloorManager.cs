using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private int bombCount = 3;
    [SerializeField]
    private Floor floorPrefab;
    [SerializeField]
    private Transform floorRoot;
    private List<Floor> floorsList = new List<Floor>();
    private float bottomFloorPosY;

    /// <summary>
    /// 最初のフロアを準備しておく
    /// その後必要に応じてSpawnFloorで追加
    /// </summary>
    public void PreparateInitFloors()
    {
        var initFloorCount = 6;
        for (int i = 0; i < initFloorCount; i++)
        {
            SpawnFloor();
        }
        bottomFloorPosY = floorsList[initFloorCount - 1].transform.position.y;
    }

    /// <summary>
    /// フロアを生成して爆弾をセットする
    /// </summary>
    /// <param name="bombCount"></param>
    private void SpawnFloor()
    {
        var floorNumber = floorsList.Count;
        var floor = Instantiate(floorPrefab, floorRoot);
        var posY = floorNumber * Constant.FLOOR_GAP * -1;
        floor.transform.position = new Vector3(0, posY, 0);
        floor.InitFloor(floorNumber,bombCount);
        floorsList.Add(floor);
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
        targetFloor.InitFloor(targetFloor.FloorNumber + 6,bombCount);
    }
}
