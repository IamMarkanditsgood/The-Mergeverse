using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : BasicScreen
{
    public Button p;
    public Button H;

    public TMP_Text timeToHome;
    private TextManager textManager = new TextManager();

    public BirdConfig[] birdConfigs;
    public Button[] read;

    void Start()
    {
        string key = "Story" + 0;
        PlayerPrefs.SetInt(key, 1);
        textManager.SetText("10", timeToHome);
        p.onClick.AddListener(Profile);
        H.onClick.AddListener(Home);


        GameEvents.OnNewTime += UdpateTimer;

        for(int i = 0; i < read.Length; i++)
        {
            int index = i;
            read[index].onClick.AddListener(() => Read(index));
        }
    }

    // Update is called once per frame
    void OnDestroy()
    {
        p.onClick.RemoveListener(Profile);
        H.onClick.RemoveListener(Home);
        GameEvents.OnNewTime -= UdpateTimer;
        for (int i = 0; i < read.Length; i++)
        {
            int index = i;
            read[index].onClick.RemoveListener(() => Read(index));
        }
    }

    public override void SetScreen()
    {
        read[0].interactable = true;
        for (int i = 1; i < read.Length; i++)
        {
            Debug.Log(i);
            read[i].interactable = false;
        }


        SetButtons();
    }

    private void SetButtons()
    {
        for(int i = 1; i < read.Length; i++)
        {
            string key = "Story" + i;
            if(PlayerPrefs.HasKey(key))
            {
                Debug.Log(key);
                read[i].interactable = true;
            }
        }
    }
    private void Read(int index)
    {
        AnimalInfo animalInfo = (AnimalInfo) UIManager.Instance.GetScreen(ScreenTypes.AnimalInfo);
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
