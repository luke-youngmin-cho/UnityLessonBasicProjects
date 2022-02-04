
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DicePlayManager : MonoBehaviour
{
    public static DicePlayManager instance;
    public CMDState CMDState;
    public List<Transform> list_MapTile;
    [SerializeField] private Transform startZone;

    [SerializeField] GameObject gameFinishedCanvas;

    private int starScore;
    private int diceNumber;
    private int goldenDiceNumber;
    private int currentSpace = -1;
    private int totalMapTile;
    private List<int> list_StarTileIndex = new List<int>();
    private void Awake()
    {
        if (instance == null) instance = this;

        starScore = 0;
        diceNumber = (int)GameSettingsData.INIT_DICE_NUMBER;
        goldenDiceNumber = (int)GameSettingsData.INIT_GOLDEN_DICE_NUMBER;

        totalMapTile = list_MapTile.Count;
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    IEnumerator Init()
    {
        yield return new WaitUntil(() => DicePlayUI.CMDState == CMDState.READY);
        RefreshUI();

        yield return new WaitUntil(() => DiceAnimationUI.CMDState == CMDState.READY);

        foreach (Transform mapTile in list_MapTile)
        {
            TileInfo_Star tmpTileInfo_Star;
            if(mapTile.TryGetComponent<TileInfo_Star>(out tmpTileInfo_Star))
            {
                list_StarTileIndex.Add(tmpTileInfo_Star.index);
            }
        }
        CMDState = CMDState.READY;

    }
    private void RefreshUI()
    {
        DicePlayUI.instance.SetDiceNumberText(diceNumber);
        DicePlayUI.instance.SetGoldenDiceNumberText(goldenDiceNumber);
        DicePlayUI.instance.SetStarScoreText(starScore);
    }
    public void RollADice()
    {
        if ((diceNumber < 1) | (CMDState != CMDState.READY)) return;
        CMDState = CMDState.BUSY;
        int diceValue = Random.Range(1, 6);
        StartCoroutine(RollADiceCoroutine(diceValue));
    }
    public void RollAGoldenDice(int diceValue)
    {
        if ((goldenDiceNumber < 1) | (CMDState != CMDState.READY)) return;
        CMDState = CMDState.BUSY;
        StartCoroutine(RollAGoldenDiceCoroutine(diceValue));
    }
    public void RollADiceReverse()
    {
        if ((diceNumber < 1) | (CMDState != CMDState.READY)) return;
        CMDState = CMDState.BUSY;
        int diceValue = Random.Range(1, 6);
        StartCoroutine(RollADiceReverseCoroutine(diceValue));
    }
    public void RollAGoldenDiceReverse(int diceValue)
    {
        if ((goldenDiceNumber < 1) | (CMDState != CMDState.READY)) return;
        CMDState = CMDState.BUSY;
        StartCoroutine(RollAGoldenDiceReverseCoroutine(diceValue));
    }
    IEnumerator RollADiceCoroutine(int diceValue)
    {
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        yield return new WaitUntil(() => DiceAnimationUI.CMDState == CMDState.READY);
        DecreaseDiceNumber();
        MovePlayer(diceValue);
        CMDState = CMDState.READY;
    }
    IEnumerator RollAGoldenDiceCoroutine(int diceValue)
    {
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        yield return new WaitUntil(() => DiceAnimationUI.CMDState == CMDState.READY);
        DecreaseGoldenDiceNumber();
        MovePlayer(diceValue);
        CMDState = CMDState.READY;
    }
    IEnumerator RollADiceReverseCoroutine(int diceValue)
    {
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        yield return new WaitUntil(() => DiceAnimationUI.CMDState == CMDState.READY);
        DecreaseDiceNumber();
        MovePlayer(-diceValue);
        DicePlayUI.instance.SwitchDicePanel();
        CMDState = CMDState.READY;
    }
    IEnumerator RollAGoldenDiceReverseCoroutine(int diceValue)
    {
        DiceAnimationUI.instance.PlayDiceAnimation(diceValue);
        yield return new WaitUntil(() => DiceAnimationUI.CMDState == CMDState.READY);
        DecreaseGoldenDiceNumber();
        MovePlayer(-diceValue);
        DicePlayUI.instance.SwitchDicePanel();
        CMDState = CMDState.READY;
    }
    private void MovePlayer(int moveSpace)
    {
        CheckPlayerPassedStarTile(currentSpace, currentSpace + moveSpace);

        currentSpace += moveSpace;
        
        if (currentSpace >= totalMapTile)
            currentSpace -= totalMapTile;

        Vector3 targetPos;
        targetPos = list_MapTile[currentSpace].position;
        Player.instance.Move(targetPos);
        OnTileEvent();
        Debug.Log(diceNumber);
        CheckGameFinished();
    }
    private void OnTileEvent()
    {
        list_MapTile[currentSpace].GetComponent<TileInfo>().onTile();
    }
    public void IncreaseDiceNumber()
    {
        diceNumber++;
        DicePlayUI.instance.SetDiceNumberText(diceNumber);
    }
    private void DecreaseDiceNumber()
    {
        diceNumber--;
        DicePlayUI.instance.SetDiceNumberText(diceNumber);
    }
    public void IncreaseGoldenDiceNumber()
    {
        goldenDiceNumber++;
        DicePlayUI.instance.SetGoldenDiceNumberText(goldenDiceNumber);
    }
    private void DecreaseGoldenDiceNumber()
    {
        goldenDiceNumber--;
        DicePlayUI.instance.SetGoldenDiceNumberText(goldenDiceNumber);
    }
    private void CheckPlayerPassedStarTile(int previousSpace, int currentSpace)
    {
        foreach (int index in list_StarTileIndex)
        {
            if ((previousSpace < index) &
                (currentSpace >= index))
            {
                int tmpStarValue = list_MapTile[index].GetComponent<TileInfo_Star>().starValue;
                starScore += tmpStarValue;
                DicePlayUI.instance.SetStarScoreText(starScore);
            }
        }
        
    }

    private void CheckGameFinished()
    {
        Debug.Log($"check dice numbers.. {diceNumber}, {goldenDiceNumber}");
        if ((diceNumber < 1) & (goldenDiceNumber < 1))
        {
            Debug.Log("Game Finished!");
            gameFinishedCanvas.SetActive(true);
        }   
    }
    public void ResetGame()
    {
        starScore = 0;
        diceNumber = (int)GameSettingsData.INIT_DICE_NUMBER;
        goldenDiceNumber = (int)GameSettingsData.INIT_GOLDEN_DICE_NUMBER;
        currentSpace = -1;
        foreach (int index in list_StarTileIndex)
        {
            list_MapTile[index].GetComponent<TileInfo_Star>().starValue = (int)GameSettingsData.INIT_STAR_TILE_VALUE;
        }

        Player.instance.Move(startZone.position);
        RefreshUI();

        DicePlayUI.instance.ActiveNormalPanel();
        DicePlayUI.instance.DeactiveReversePanel();
    }
}
