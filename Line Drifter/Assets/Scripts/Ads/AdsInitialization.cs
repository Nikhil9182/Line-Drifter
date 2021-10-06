using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using System.Net;

public class AdsInitialization : MonoBehaviour, IUnityAdsInitializationListener
{
    string _androidGameId = "4388169";
    string _iOsGameId = "4388168";
    [SerializeField] bool _testMode = true;
    [SerializeField] bool _enablePerPlacementMode = true;
    private string _gameId;

    private static AdsInitialization _instance;

    public static AdsInitialization Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsGameId
            : _androidGameId;
        if(!Advertisement.isInitialized && HasConnection())
        {
            Advertisement.Initialize(_gameId, _testMode, _enablePerPlacementMode, this);
        }
    }

    public void OnInitializationComplete()
    {

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        //Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public bool HasConnection()
    {
        try
        {
            using (var client = new WebClient())
            using (var stream = new WebClient().OpenRead("http://www.google.com"))
            {
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
