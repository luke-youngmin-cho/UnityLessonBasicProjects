using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DicePlayUI : MonoBehaviour
{
    public static DicePlayUI instance;
    public static CMDState CMDState;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CMDState = CMDState.READY;
        }
    }

    public TMP_Text starScoreText;
    [SerializeField]private TMP_Text diceNumberText;
    [SerializeField]private TMP_Text goldenDiceNumberText;
    [SerializeField]private GameObject normalPanel;
    [SerializeField]private GameObject reversePanel;

    public void SwitchDicePanel()
    {
        normalPanel.SetActive(!normalPanel.activeSelf);
        reversePanel.SetActive(!reversePanel.activeSelf);
    }
    public void ActiveNormalPanel()
    {
        normalPanel.SetActive(true);
    }
    public void DeactiveReversePanel()
    {
        reversePanel.SetActive(false);
    }

    public void SetStarScoreText(int starScore)
    {
        starScoreText.text = starScore.ToString();
    }
    public void SetDiceNumberText(int diceNumber)
    {
        diceNumberText.text = diceNumber.ToString();
    }
    public void SetGoldenDiceNumberText(int goldenDiceNumber)
    {
        goldenDiceNumberText.text = goldenDiceNumber.ToString();
    }   
}
