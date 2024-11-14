using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultCanvas : MonoBehaviour
{
    [SerializeField]
    private GameObject title_win, title_lose;
    [SerializeField]
    private TextMeshProUGUI rewardAmountText;
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private Button retryButton;
    private bool didRetry;

    private void Start()
    {
        retryButton.onClick.AddListener(OnClickRetryButton);
    }
    public void ActivateWinPanel(int rewardAmount)
    {
        gameObject.SetActive(true);
        filter.SetActive(true);
        title_win.SetActive(true);
        title_lose.SetActive(false);
        UpdateRewardAmountText(rewardAmount);
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
        filter.SetActive(true);
        title_lose.SetActive(true);
        title_win.SetActive(false);
    }

    private void UpdateRewardAmountText(int rewardAmount)
    {
        rewardAmountText.text = rewardAmount.ToString();
    }
}
