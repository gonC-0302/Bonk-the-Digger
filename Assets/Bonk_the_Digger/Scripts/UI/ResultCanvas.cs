using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject title_win, title_lose;
    [SerializeField]
    private TextMeshProUGUI cashBackAmountText;
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Animator anim;
    private bool didRetry;

    private void Start()
    {
        retryButton.onClick.AddListener(OnClickRetryButton);
    }
    public void ActivateWinPanel(int cashBackAmount)
    {
        gameObject.SetActive(true);
        anim.Play("WinPanelAnimation");
        filter.SetActive(true);
        title_win.SetActive(true);
        title_lose.SetActive(false);
        UpdateRewardAmountText(cashBackAmount);
    }
    private void OnClickRetryButton()
    {
        if (didRetry) return;
        didRetry = true;
        SceneManager.LoadSceneAsync("Game");
    }
    public void ActivateLosePanel()
    {
        gameObject.SetActive(true);
        anim.Play("LosePanelAnimation");
        filter.SetActive(true);
        //title_lose.SetActive(true);
        title_win.SetActive(false);
    }

    private void UpdateRewardAmountText(int cashBackAmount)
    {
        cashBackAmountText.text = cashBackAmount.ToString();
    }
}
