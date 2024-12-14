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
    private Tile challengeBoxTile;

    void Update()
    {
        switch (gameManager.CurrentPhase)
        {
            case GamePhase.SelectTile:
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
                        if(tile.Type == TileType.ChallengeBox) challengeBoxTile = tile;
                        character.MoveTargetTile(tile);
                    }
                }
                break;
            case GamePhase.BonusTap:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float maxDistance = 10;
                    RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, maxDistance);
                    if (!hit.collider) return;
                    if (hit.collider.TryGetComponent<Tile>(out Tile tile))
                    {
                        if (tile != challengeBoxTile) return;
                        tile.UpdateTapCount();
                        SoundManager.instance.PlaySE(SoundType.TapChallengeBox);
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
