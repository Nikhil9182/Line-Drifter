﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int coinsAmount = 50;
    public int SelectedPen = 0;
    public int SelectedCar = 0;
    public int[] levels = new int[100];
    public int[] hints = new int[100];
    public int[] carsbuy = new int[10];
    public int[] pensbuy = new int[10];
    public int[] paintbuy = new int[10];
    public int[] levelStars = new int[100];
    public bool isLoaded = true;
}

public class Game : MonoBehaviour
{
    public GameData gameData = new GameData();

    #region BuiltIn Methods
    private void Awake()
    {
        LoadGameData();
        if(gameData.isLoaded)
        {
            gameData.hints[0] = 1;
            gameData.levels[0] = 1;
            gameData.carsbuy[0] = 1;
            gameData.pensbuy[0] = 1;
            gameData.paintbuy[0] = 1;
            SaveGameData();
        }
    }
    private void Start()
    {
        if(gameData.isLoaded)
        {
            for (int i = 1; i < gameData.levels.Length; i++)
            {
                if (gameData.levels[i] != 1)
                {
                    gameData.levels[i] = 0;
                }
            }
            gameData.isLoaded = false;
            SaveGameData();
        }
        AdsInitialization.Instance.InitializeAds();
    }
    #endregion

    #region Custom Methods
    public void IncreaseCoins(int coinsWonAmount)
    {
        gameData.coinsAmount += coinsWonAmount;
    }

    public void DecreaseCoins(int coinsWantToUse)
    {
        gameData.coinsAmount -= coinsWantToUse;
    }
    public void LoadGameData()
    {
        gameData = BinarySerializer.Load<GameData>("GameData.nb");
    }
    public void SaveGameData()
    {
        BinarySerializer.Save<GameData>(gameData, "GameData.nb");
    }
    public void UnlockNextLevel(int index)
    {
        gameData.levels[index] = 1;
    }

    public bool CheckCoins(int coins)
    {
        if(gameData.coinsAmount >= coins)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnlockPen(int i)
    {
        gameData.pensbuy[i] = 1;
        SaveGameData();
    }

    public void UnlockPaint(int i)
    {
        gameData.paintbuy[i] = 1;
        SaveGameData();
    }

    public bool Check()
    {
        if(gameData.levels[9] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
