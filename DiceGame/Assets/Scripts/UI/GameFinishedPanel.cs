using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameFinishedPanel : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private void OnEnable()
    {
        scoreText.text = DicePlayUI.instance.starScoreText.text;
    }
}
