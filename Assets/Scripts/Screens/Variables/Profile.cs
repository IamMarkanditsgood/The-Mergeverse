using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    public Button s;
    public Button H;
    public Button i;
    public Button avatar;
    public TMP_Text timeToHome;
    public TMP_Text coins;
    private TextManager textManager = new TextManager();
    public AvatarManager avatarManager;
    public TMP_InputField name;

    public Image[] achievementsImage;
    public Sprite[] openedAchievements;


    void Start()
    {
        textManager.SetText(PlayerPrefs.GetInt("Coins"), coins, true);
        textManager.SetText("10", timeToHome);
        s.onClick.AddListener(Shop);
        H.onClick.AddListener(Home);
        i.onClick.AddListener(Info);
        avatar.onClick.AddListener(Avatar);

        GameEvents.OnNewCoins += UpdateCoins;
        GameEvents.OnNewTime += UdpateTimer;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        s.onClick.RemoveListener(Shop);
        H.onClick.RemoveListener(Home);
        i.onClick.RemoveListener(Info);
        avatar.onClick.RemoveListener(Avatar);

        GameEvents.OnNewCoins -= UpdateCoins;
        GameEvents.OnNewTime -= UdpateTimer;
    }
    public void UpdateCoins(int Coins)
    {
        textManager.SetText(Coins, coins, true);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("Name", name.text);
    }

    public override void SetScreen()
    {

        avatarManager.SetSavedPicture();
        name.text = PlayerPrefs.GetString("Name", "USER_NAME");
        SetAchievements();
    }


    public void UdpateTimer(int time)
    {
        textManager.SetText(time, timeToHome);
    }

    public override void ResetScreen()
    { 
    }



    private void SetAchievements()
    {
        for(int i = 0; i < achievementsImage.Length; i++)
        {
            string key = "Achieve" + i;
            if (PlayerPrefs.GetInt(key) == 1)
            {
                achievementsImage[i].sprite = openedAchievements[i];
            }
        }
    }
    private void Shop()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
        PlayerPrefs.SetString("Name", name.text);
    }

    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
        PlayerPrefs.SetString("Name", name.text);
    }

    private void Info()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
        PlayerPrefs.SetString("Name", name.text);
    }
    private void Avatar()
    {
        avatarManager.PickFromGallery();
    }
}
