using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Unity.Collections.LowLevel.Unsafe;

public class BetPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeField]
    private Button okButton;
    [SerializeField]
    private List<Button> bombButtonsList = new List<Button>();
    [SerializeField]
    private GameObject cashOutPanel;
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

    private void Awake()
    {
        // イベント登録
        _inputField.onEndEdit.AddListener(OnEndEdit);
        _inputField.onValueChanged.AddListener(OnValueChanged);
        okButton.interactable = false;
        okButton.onClick.AddListener(OnClickOKButton);
    }

    private void OnValueChanged(string betValue)
    {
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
        }
        else
        {
            okButton.interactable = true;
        }
    }

    private void OnEndEdit(string betValueString)
    {
        if (string.IsNullOrEmpty(betValueString)) return;
        var betValue = int.Parse(betValueString);
        betValueCache = betValue;
    }
    /// <summary>
    /// 爆弾の数を選択
    /// </summary>
    /// <param name="bombButtonID"></param>
    public void SelectBombCount(int bombButtonID)
    {
        for (int i = 0; i < bombButtonsList.Count; i++)
        {
            bombButtonsList[i].image.color = Color.black;
        }
        for (int i = 0; i < bombButtonID; i++)
        {
            bombButtonsList[i].image.color = Color.white;
        }
        bombCountCache = bombButtonID;
    }
    private void OnClickOKButton()
    {
        cashOutPanel.SetActive(true);
        gameObject.SetActive(false);
        cinemachinePositionComposer.TargetOffset = new Vector3(0,-1.25f,0);
        cashManager.SetBet(betValueCache);
        floorManager.SetBombsInitialFloor(bombCountCache);
        gameManager.StartGame();
    }
}
