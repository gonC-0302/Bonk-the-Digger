using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject headerItems;

    public void ShowHeaderItems()
    {
        headerItems.SetActive(true);
    }

    public void HideHeaderItems()
    {
        headerItems.SetActive(false);
    }
}
