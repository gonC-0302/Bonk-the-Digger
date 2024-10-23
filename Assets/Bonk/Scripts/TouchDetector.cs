using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private Character character;
    void Update()
    {
        if (manager.CurrentState != GameState.SelectTile) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float maxDistance = 10;
            RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
            if (!hit.collider) return;
            if (hit.collider.TryGetComponent<Tile>(out Tile tile))
            {
                // 現在滞在しているフロアのタイルしかタップできないように制限
                if (tile.FloorNumber != manager.CurrentFloorNumber) return;
                //Debug.Log($"IsBomb{tile.IsBomb}");
                DetectTile(tile);
            }
        }
    }
    /// <summary>
    /// タイルを検知成功
    /// </summary>
    /// <param name="tile"></param>
    private void DetectTile(Tile tile)
    {
        character.MoveTargetTile(tile);
    }
}
