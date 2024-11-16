using UnityEngine;
using UnityEngine.UI;

public class CashOutPanel : MonoBehaviour
{
    [SerializeField]
    private Button cashOutButton,cancelButton,confirmButton;
    [SerializeField]
    private GameObject popUp;
    [SerializeField]
    private GameManager manager;
    [SerializeField]
    private Character character;

    public void ActivateCashOutButton()
    {
        cashOutButton.interactable = true;
    }

    public void DeactivateCashOutButton()
    {
        cashOutButton.interactable = false;
    }

    private void Start()
    {
        cashOutButton.onClick.AddListener(OnClickCashOutButton);
        cancelButton.onClick.AddListener(OnClickCancelButton);
        confirmButton.onClick.AddListener(OnClickConfirmButton);
    }
    private void OnClickCashOutButton()
    {
        manager.PauseGame();
        OpenPopUp();
    }
    private void OpenPopUp()
    {
        popUp.SetActive(true);
    }
    private void ClosePopUp()
    {
        popUp.SetActive(false);
    }
    private void OnClickCancelButton()
    {
        manager.ResumeGame();
        ClosePopUp();
    }
    private void OnClickConfirmButton()
    {
        manager.Win();
        character.PlayWinAnimation();
    }
    public void CloseCashOutPanel()
    {
        gameObject.SetActive(false);
    }
}