using UnityEngine;
using UnityEngine.UI;

public class CashOutPanel : MonoBehaviour
{
    [SerializeField]
    private Button cashOutButton,cancelButton,confirmButton;
    [SerializeField]
    private GameObject popUp,filter;
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private Character character;

    private void Start()
    {
        cashOutButton.onClick.AddListener(OnClickCashOutButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }
    private void OnClickCashOutButton()
    {
        SoundManager.instance.PlaySE(SoundType.CashOutButton);
        manager.PauseGame();
        OpenPopUp();
    }
    private void OpenPopUp()
    {
        popUp.SetActive(true);
        filter.SetActive(true);
    }
    private void ClosePopUp()
    {
        popUp.SetActive(false);
        filter.SetActive(false);
    }
    private void OnClickCancelButton()
    {
        manager.ResumeGame();
        ClosePopUp();
    }
    private void OnClickConfirmButton()
    {
        popUp.SetActive(false);
        filter.SetActive(false);
        manager.Win();
        character.PlayWinAnimation();
    }
    public void DisableCashOutPanel()
    {
        gameObject.SetActive(false);
    }
    public void ActivateCashOutButton()
    {
        cashOutButton.interactable = true;
    }
    public void DeactivateCashOutButton()
    {
        cashOutButton.interactable = false;
    }
}
