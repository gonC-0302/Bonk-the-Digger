using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class BetPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeField]
    private Image inputBackground;
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private List<Button> bombButtonsList = new List<Button>();
    [SerializeField]
    private Sprite bombButton_on, bombButton_off;
    [SerializeField]
    private GameObject cashOutPanel;
    [SerializeField]
    private Animator topCanvasAnim;
    [SerializeField]
    private CinemachinePositionComposer cinemachinePositionComposer;
    [SerializeField]
    private FloorManager floorManager;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private CashManager cashManager;
    private int bombCountCache = 1;
    private int betValueCache = 0;
    [SerializeField]
    private Button halfButton, doubleButton, maxButton;
    private const int MAX_BET_AMOUNT = 9999999;

    private void Awake()
    {
        // イベント登録
        _inputField.onEndEdit.AddListener(OnEndEdit);
        _inputField.onValueChanged.AddListener(OnValueChanged);
        _inputField.onSelect.AddListener(OnSelect);
        okButton.interactable = false;
        okButton.onClick.AddListener(OnClickOKButton);
        halfButton.onClick.AddListener(OnClickHalfButton);
        doubleButton.onClick.AddListener(OnClickDoubleButton);
        maxButton.onClick.AddListener(OnClickMaxButton);
        halfButton.interactable = false;
        doubleButton.interactable = false;
    }

    private void OnClickHalfButton()
    {
        float halfValue = Mathf.Clamp(betValueCache * 0.5f, 0, MAX_BET_AMOUNT);
        betValueCache = (int)halfValue;
        _inputField.text = betValueCache.ToString();
    }

    private void OnClickDoubleButton()
    {
        int doubleValue = Mathf.Clamp(betValueCache * 2,0, MAX_BET_AMOUNT);
        betValueCache = doubleValue;
        _inputField.text = betValueCache.ToString();
    }

    private void OnSelect(string betValue)
    {
        inputBackground.enabled = true;
    }

    private void OnClickMaxButton()
    {
        int maxValue = MAX_BET_AMOUNT;
        betValueCache = maxValue;
        _inputField.text = betValueCache.ToString();
    }

    private void OnValueChanged(string betValueStr)
    {
        if (string.IsNullOrEmpty(betValueStr))
        {
            _inputField.text = "";
        }
        if (betValueStr.StartsWith("0"))
        {
            _inputField.text = "";
        }

        // 桁数上限
        if (_inputField.text.Length > Constant.MAX_BET_DIGIT)
        {
            _inputField.text = _inputField.text[..Constant.MAX_BET_DIGIT];
        }

        // TODO: 所持金でかけれる上限

        // 未入力制限
        if (string.IsNullOrEmpty(_inputField.text))
        {
            okButton.interactable = false;
            halfButton.interactable = false;
            doubleButton.interactable = false;
        }
        else
        {
            okButton.interactable = true;
            halfButton.interactable = true;
            doubleButton.interactable = true;
        }
    }

    private void OnEndEdit(string betValueString)
    {
        inputBackground.enabled = false;
        if (string.IsNullOrEmpty(betValueString)) return;
        int betAmount = Mathf.Clamp(int.Parse(betValueString),0, MAX_BET_AMOUNT);
        _inputField.text = betAmount.ToString();
        betValueCache = betAmount;
    }
    /// <summary>
    /// 爆弾の数を選択
    /// </summary>
    /// <param name="bombButtonID"></param>
    public void SelectBombCount(int bombButtonID)
    {
        for (int i = 0; i < bombButtonsList.Count; i++)
        {
            bombButtonsList[i].image.sprite = bombButton_off;
        }
        for (int i = 0; i < bombButtonID; i++)
        {
            bombButtonsList[i].image.sprite =bombButton_on;
        }
        bombCountCache = bombButtonID;
        SoundManager.instance.PlaySE(SoundType.SelectBombCount);
    }
    private void OnClickOKButton()
    {
        cashOutPanel.SetActive(true);
        //gameObject.SetActive(false);
        topCanvasAnim.enabled = true;
        cinemachinePositionComposer.TargetOffset = new Vector3(0,0,0);
        // 爆弾の数を設定
        floorManager.SetBombsInitialFloor(bombCountCache);
        cashManager.SetBet(betValueCache);
        gameManager.StartGame();
    }
}