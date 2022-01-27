using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    static public Player instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        HP = initHP;
    }
    private float _HP;
    public float HP
    {
        set
        {
            float tmpValue = value;
            if (tmpValue <= 0)
            {
                tmpValue = 0;
                Destroy(this.gameObject);
            }   
            _HP = tmpValue;
            HPBar.value = _HP / initHP;
        }
        get
        {
            return _HP;
        }
    }
    [SerializeField] private float initHP = 100;
    [SerializeField] private Slider HPBar;

    private float _score;
    public float score
    {
        set
        {
            float tmpValue = value;
            _score = tmpValue;
            tmpValue = (int)_score;
            scoreText.text = tmpValue.ToString();
        }
        get
        {
            return _score;
        }
    }
    [SerializeField] private Text scoreText;
    
}
