
using UnityEngine;
using UnityEngine.UI;

public class Onboarding : BasicScreen
{
    public GameObject[] panels;
    public Button[] next;
    public Button start;
    public GameManager gameManager;
    private int currentPane;

    public void Start()
    {
        start.onClick.AddListener(Play);

        for(int i =0; i < next.Length; i++)
        {
            int index = i;
            next[index].onClick.AddListener(Next);
        }
    }

    private void OnDestroy()
    {
        start.onClick.RemoveListener(Play);

        for (int i = 0; i < next.Length; i++)
        {
            int index = i;
            next[index].onClick.RemoveListener(Next);
        }
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        panels[currentPane].SetActive(true);
    }

    private void Next()
    {
        if (currentPane < panels.Length - 1)
        {
            currentPane++;
            panels[currentPane - 1].SetActive(false);
            panels[currentPane].SetActive(true);
        }
    }

    private void Play()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
        gameManager.StartGame();
        PlayerPrefs.SetInt("FirstGame",1);
    }
}
