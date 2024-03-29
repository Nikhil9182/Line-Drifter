﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    [SerializeField] private GameObject[] paintObjects;
    [SerializeField] private TextMeshProUGUI[] paintItemsText;
    [SerializeField] private GameObject[] paintBuyButtons;
    [SerializeField] private GameObject[] paintSelectButtons;

    private int point = 0;

    private void Awake()
    {
        LoadPaintObjects();
    }

    private void Start()
    {
        UpdatePaintUI();
    }

    private void LoadPaintObjects()
    {
        for (int i = 0; i < paintObjects.Length; i++)
        {
            paintItemsText[i] = paintObjects[i].transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            paintBuyButtons[i] = this.paintObjects[i].transform.GetChild(1).gameObject;
            paintSelectButtons[i] = this.paintObjects[i].transform.GetChild(2).gameObject;
        }
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
        paintSelectButtons[point].GetComponent<Button>().interactable = true;
        point = position;
        paintSelectButtons[position].GetComponent<Button>().interactable = false;
        GameManager.Instance.selectedLinePrefab = paint[position].prefab;

    }

    public void BuyItem(int i)
    {
        if (paint[i].price <= game.gameData.coinsAmount)
        {
            paintBuyButtons[i].SetActive(false);
            paintSelectButtons[i].SetActive(true);
            game.DecreaseCoins(paint[i].price);
            game.UnlockPaint(i);
            menu.UpdateCoins(paint[i].price);
            game.SaveGameData();
        }
    }
}
