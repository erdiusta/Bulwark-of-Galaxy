using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class AdMaker : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] bool testMode = true;

    public static AdMaker Instance;

    LevelManager levelManager;

#if UNITY_ANDROID
    string gameId = "5208205";
#elif UNITY_IOS
    string gameId = "5208204";
#else
    string gameId = "Not Available";
#endif

    void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        Advertisement.Load("rewardedVideo", this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }

    public void ShowAd(LevelManager levelManager) 
    {
        this.levelManager = levelManager;
        Advertisement.Show("rewardedVideo", this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) 
    {
        Advertisement.Load("rewardedVideo", this);
        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.COMPLETED:
                levelManager.ContinueGame();
                levelManager.LoadNextScene();
                break;
            case UnityAdsShowCompletionState.SKIPPED:
                // Ad was skipped
                break;
            case UnityAdsShowCompletionState.UNKNOWN:
                Debug.LogWarning("Ad Failed");
                break;
        }
    }
}
