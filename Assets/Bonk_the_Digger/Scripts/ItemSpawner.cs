using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject itemCoinPrefab;
    private GameObject itemCoin;

    public void SpawnItemCoin(Vector3 spawnPos)
    {
        if(itemCoin == null)
        {
            itemCoin = Instantiate(itemCoinPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            itemCoin.transform.position = spawnPos;
            var anim = itemCoin.GetComponent<Animator>();
            anim.Play("GetCoin");
        }
    }
}
