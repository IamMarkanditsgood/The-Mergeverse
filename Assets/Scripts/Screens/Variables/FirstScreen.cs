using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FirstScreen : BasicScreen
{
    public Button a;
    public GameManager gameManager;

    private void Start()
    {
        a.onClick.AddListener(ScreenGame);


    }
    private void OnDestroy()
    {
        a.onClick.RemoveListener(ScreenGame);
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }

    private void ScreenGame()
    {
        if(PlayerPrefs.GetInt("FirstGame") == 1)
        {
            UIManager.Instance.ShowScreen(ScreenTypes.Game);
            gameManager.StartGame();
        }
        else
        {
            UIManager.Instance.ShowScreen(ScreenTypes.Onbording);
        }
       
    }
}
