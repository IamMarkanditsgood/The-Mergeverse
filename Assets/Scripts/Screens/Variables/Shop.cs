using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : BasicScreen
{
    public Button p;
    public Button H;

    public TMP_Text coins;
    public TMP_Text timeToHome;
    private TextManager textManager = new TextManager();

    public BirdConfig[] birdConfigs;
    public Button[] buy;
    public Button[] read;
    public int[] prices;

    void Start()
    {
        textManager.SetText(PlayerPrefs.GetInt("Coins"), coins, true);
        textManager.SetText("10", timeToHome);
        p.onClick.AddListener(Profile);
        H.onClick.AddListener(Home);

        GameEvents.OnNewCoins += UpdateCoins;
        GameEvents.OnNewTime += UdpateTimer;

        for(int i = 0; i < read.Length; i++)
        {
            int index = i;
            read[index].onClick.AddListener(() => Read(index));
        }
        for (int i = 0; i < buy.Length; i++)
        {
            int index = i;
            buy[index].onClick.AddListener(() => Buy(index));
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        p.onClick.RemoveListener(Profile);
        H.onClick.RemoveListener(Home);

        GameEvents.OnNewCoins -= UpdateCoins;
        GameEvents.OnNewTime -= UdpateTimer;

        for (int i = 0; i < read.Length; i++)
        {
            int index = i;
            read[index].onClick.RemoveListener(() => Read(index));
        }
        for (int i = 0; i < buy.Length; i++)
        {
            int index = i;
            buy[index].onClick.RemoveListener(() => Buy(index));
        }

    }
    public void UpdateCoins(int Coins)
    {
        textManager.SetText(Coins, coins, true);
    }
    public override void SetScreen()
    {


        SetButtons();
    }

    private void SetButtons()
    {
        for(int i = 0; i < read.Length; i++)
        {
            read[i].gameObject.SetActive(false);
            buy[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < read.Length; i++)
        {
            string key = "Story" + i;
            if(PlayerPrefs.GetInt(key) == 1)
            {
                Debug.Log(key);
                read[i].gameObject.SetActive(true);
                buy[i].gameObject.SetActive(false);
            }
        }
    }
    private void Buy(int index)
    {
        int coins = PlayerPrefs.GetInt("Coins");
        if (coins >= prices[index])
        {
            Debug.Log("Good");
            coins -= prices[index];
            PlayerPrefs.SetInt("Coins", coins);
            GameEvents.NewCoins(coins);
            string key = "Story" + index;
            if (!PlayerPrefs.HasKey(key))
            {
                Debug.Log(key);
                PlayerPrefs.SetInt(key, 1);
            }
            SetButtons();
        }

        
    }

    private void Read(int index)
    {
        AnimalInfo animalInfo = (AnimalInfo)UIManager.Instance.GetScreen(ScreenTypes.AnimalInfo);
        animalInfo.Init(birdConfigs[index]);
        UIManager.Instance.ShowScreen(ScreenTypes.AnimalInfo);
    }

    public void UdpateTimer(int time)
    {
        textManager.SetText(time, timeToHome);
    }

    public override void ResetScreen()
    {
    }

    private void Profile()
    { 
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }

    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
    }
}
