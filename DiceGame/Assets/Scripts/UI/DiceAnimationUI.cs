using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text.RegularExpressions;
public class DiceAnimationUI : MonoBehaviour
{
    public static DiceAnimationUI instance;
    public static CMDState CMDState;
    private List<Sprite> list_DiceSprite = new List<Sprite>();

    [SerializeField] Image diceAnimImage;
    [SerializeField] GameObject diceRollFinishEffect;
    private float diceAnimationTime;
    private float diceAnimationFinishDelay;
    private Coroutine coroutine;
    private void Awake()
    {
        if (instance == null) instance = this;

        diceAnimationTime = (float)GameSettingsData.DICE_ANIMATION_TIME/10;
        diceAnimationFinishDelay = (float)GameSettingsData.DICE_ANIMATION_FINISH_DELAY/10;
    }
    private void Start()
    {
        StartCoroutine(InitCoroutine());
    }
    IEnumerator InitCoroutine()
    {
        LoadDiceImages();
        CMDState = CMDState.READY;
        yield return null;
    }
    private void LoadDiceImages()
    {
        //list_DiceImage.AddRange(Resources.LoadAll<Image>("/DiceImages")); // System.Linq 를 추가하면 .ToList<> 사용가능
        Sprite[] sprites = Resources.LoadAll<Sprite>("DiceImages");
        sprites.OrderBy(file => int.Parse(Regex.Replace(file.name, @"\D","")));
        list_DiceSprite = sprites.ToList();
    }

    public void PlayDiceAnimation(int diceValue)
    {
        CMDState = CMDState.BUSY;
        diceAnimImage.color = new Color(1f, 1f, 1f);
        coroutine = StartCoroutine(DiceAnimationCoroutine(diceValue));
    }
    public void PlayGoldenDiceAnimation(int diceValue)
    {
        CMDState = CMDState.BUSY;
        diceAnimImage.color = new Color(1f, 0.95f, 0);
        coroutine = StartCoroutine(DiceAnimationCoroutine(diceValue));
    }
    IEnumerator DiceAnimationCoroutine(int diceValue)
    {   
        diceAnimImage.gameObject.SetActive(true);
        float timeLapse = 0;
        while (timeLapse < diceAnimationTime)
        {
            timeLapse += diceAnimationTime/10;
            int tmpIdx = Random.Range(0, list_DiceSprite.Count);
            diceAnimImage.sprite = list_DiceSprite[tmpIdx];
            yield return new WaitForSeconds(timeLapse);
        }
        diceAnimImage.sprite = list_DiceSprite[diceValue - 1];
        diceRollFinishEffect.SetActive(true);
        yield return new WaitForSeconds(diceAnimationFinishDelay);
        

        coroutine = null;
        CMDState = CMDState.READY;
    }

}
