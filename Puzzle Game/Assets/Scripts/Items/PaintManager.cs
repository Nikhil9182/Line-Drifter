using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PaintItems
{
    public GameObject prefab;
    public int price;
}

public class PaintManager : MonoBehaviour
{
    public PenItems[] paint;

    [SerializeField] private Game game;
    [SerializeField] private UIMenu menu;
    [SerializeField] private TextMeshProUGUI[] paintItemsText;
    [SerializeField] private GameObject[] paintBuyButtons;
    [SerializeField] private GameObject[] paintSelectButtons;

    private GameManager manager;

    private void Start()
    {
        UpdatePaintUI();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void UpdatePaintUI()
    {
        for (int i = 0; i < paint.Length; i++)
        {
            if (game.gameData.paintbuy[i] == 1)
            {
                paintBuyButtons[i].SetActive(false);
                paintSelectButtons[i].SetActive(true);
            }
            else if (game.gameData.paintbuy[i] == 0)
            {
                paintSelectButtons[i].SetActive(false);
                paintBuyButtons[i].SetActive(true);
                paintItemsText[i].text = paint[i].price.ToString();
            }
        }
    }

    public void SelectItem(int position)
    {
        manager.selectedLinePrefab = paint[position].prefab;
    }

    public void BuyItem(int i)
    {
        if (paint[i].price <= game.gameData.coinsAmount)
        {
            paintBuyButtons[i].SetActive(false);
            paintSelectButtons[i].SetActive(true);
            game.DecreaseCoins(paint[i].price);
            game.UnlockPaint(i);
            menu.UpdateCoins();
            game.SaveGameData();
        }
    }
}
