using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeChicken : MonoBehaviour
{
    public int Level { get; private set; }

    [SerializeField] private Sprite[] levelSprites;
    public int MaxLevel => levelSprites.Length;
    private SpriteRenderer spriteRenderer;


    [SerializeField] private int profitPerSecond = 1; // ������

    public int GetProfitPerSecond() => profitPerSecond;

    // ���� ����� �������� ������:
    public void SetProfit(int amount) => profitPerSecond = amount;
    [SerializeField] private int[] profitPerLevel = new int[] { 1, 2, 5, 10, 25, 50 };

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int level)
    {
        Level = level;
        spriteRenderer.sprite = levelSprites[Level - 1];
        profitPerSecond = profitPerLevel[Mathf.Clamp(level - 1, 0, profitPerLevel.Length - 1)];
    }

    public void Upgrade()
    {
        if (Level < MaxLevel)
        {
            Debug.Log(PlayerPrefs.GetInt("Achieve0"));

            Debug.Log(PlayerPrefs.GetInt("Achieve0"));
            Init(Level + 1);
        }
        else
        {
            GameManager.Instance.AddCoins(200);
            GameManager.Instance.RemoveChicken(this);
            Destroy(gameObject);
        }
    }

}
