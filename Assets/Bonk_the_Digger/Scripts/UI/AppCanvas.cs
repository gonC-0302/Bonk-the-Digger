using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject headerItems;     // 所持金・戻るボタンなど

    public void ShowHeaderInfomations()
    {
        headerItems.SetActive(true);
    }
    public void HideHeaderInfomations()
    {
        headerItems.SetActive(false);
    }
}
