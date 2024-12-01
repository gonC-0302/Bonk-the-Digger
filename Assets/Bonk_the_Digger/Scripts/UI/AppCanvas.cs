using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject headerItems;

    public void ShowHeaderInfomations()
    {
        headerItems.SetActive(true);
    }

    public void HideHeaderInfomations()
    {
        headerItems.SetActive(false);
    }
}
