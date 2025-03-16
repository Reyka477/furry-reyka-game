using System.Collections;
using System.Collections.Generic;
using MyGame;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartBattle : MonoBehaviour
{
    public Button battleButton;
    public TMP_Text buttonText;
    private bool _isStarted = false;
    public Sprite startSprite;
    public Sprite stopSprite;
    public Image buttonImage;
    public BattleManager battleManager;


    public void Start()
    {
        // Навешиваем функцию при клике на battleButton
        battleButton.onClick.AddListener(ToggleButton);
    }

    void ToggleButton()
    {
        _isStarted = !_isStarted;
        if (_isStarted)
        {
            buttonText.text = "Stop";
            buttonImage.sprite = stopSprite;
            battleManager.StartBattle();
        }
        else
        {
            buttonText.text = "Start";
            buttonImage.sprite = startSprite;
        }
    }
}