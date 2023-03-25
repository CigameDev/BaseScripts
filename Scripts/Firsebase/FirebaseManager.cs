
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using System;
using Firebase.Extensions;
using Firebase.Analytics;
//using AppsFlyerSDK;
using Firebase.Crashlytics;
using AppsFlyerSDK;
using Sonat;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    public bool firebaseInitialized = false;
    public bool getRemoteConfigDone = false;


    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                firebaseInitialized = true;
                FirebaseApp app = FirebaseApp.DefaultInstance;
                SonatAnalyticTracker.FirebaseReady = true;
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });


    }

    private void InitializeFirebase()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
    }
}
