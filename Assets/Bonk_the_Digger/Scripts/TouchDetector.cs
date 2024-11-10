using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private Character character;
    private Tile bonusTile;

    void Update()
    {
        switch (gameManager.CurrentState)
        {
            case GameState.SelectTile:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float maxDistance = 10;
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
                    if (!hit.collider) return;
                    if (hit.collider.TryGetComponent<Tile>(out Tile tile))
                    {
                        // 現在滞在しているフロアのタイルしかタップできないように制限
                        if (tile.FloorNumber != floorManager.CurrentFloorNumber) return;
                        if(tile.Type == TileType.Bonus) bonusTile = tile;
                        character.MoveTargetTile(tile);
                    }
                }
                break;
            case GameState.BonusTap:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float maxDistance = 10;
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
                    if (!hit.collider) return;
                    if (hit.collider.TryGetComponent<Tile>(out Tile tile))
                    {
                        if (tile != bonusTile) return;
                        tile.UpdateTapCount();
                        SoundManager.instance.PlaySE(SoundType.TapTreasureBox);
                        if (tile.TapCount > 10)
                        {
                            gameManager.ChangeToMoveCharacterState();
                            tile.HideChallengeBox();
                            StartCoroutine(character.GetTreasureBox(tile));
                        }
                    }
                }
                break;
        }
    }
}
