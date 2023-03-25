using System;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    private static AdManager instance;
    public static AdManager Instance => instance;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private bool _showing;
    UnityAction rewardedCallback;
    UnityAction interstitialedCallback;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this as AdManager;
            DontDestroyOnLoad(gameObject);
            //khong pha huy doi tuong nay khi load lai scene
        }
    }
    void Start()
    {
        //Khoi chay SDK quang cao tren dien thoai di dong cua google
        MobileAds.Initialize(initStatus => { });
        this.RequestBanner();
        this.RequestInterstitial();
        this.RequestRewardAd();
    }
    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
        //BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, 0, 50);
        //AdSize adSize = new AdSize(250, 250);
        //BannerView bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);

        // tao va yeu cau quang cao trong
        AdRequest request = new AdRequest.Builder().Build();
        //tai banner voi yeu cau
        this.bannerView.LoadAd(request);
    }
    #region Interstitial
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

        //quang cao inter thi chi nen dong quang cao thi duoc thuc hien cau lenh
        //phia sau,khong can xem het quang cao
        this.interstitial.OnAdClosed += OnAdFullScreenContentClosed;
        this.interstitial.OnAdLoaded += HandleOnInterstitialAdLoaded;
        this.interstitial.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoad;
        this.interstitial.OnAdFailedToShow += Interstitial_OnAdFailedToShow;
        this.interstitial.OnAdDidRecordImpression += Interstitial_OnAdDidRecordImpression;
        this.interstitial.OnAdOpening += Interstitial_OnAdOpening;

    }

    private void Interstitial_OnAdOpening(object sender, EventArgs e)
    {

    }

    private void Interstitial_OnAdDidRecordImpression(object sender, EventArgs e)
    {

    }

    private void Interstitial_OnAdFailedToShow(object sender, AdErrorEventArgs e)
    {

    }

    public void OnAdFullScreenContentClosed(object sender, EventArgs args)
    {
        interstitialedCallback?.Invoke();
        RequestInterstitial();
    }
    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("xu ly khi quang cao dc goi thanh cong");
    }
    public void HandleOnInterstitialAdFailedToLoad(object sender, EventArgs args)
    {
        Debug.Log("Xu ly khi quang cao goi that bai");
    }
    public bool IsInterstitialLoaded()
    {
        return this.interstitial.IsLoaded();
    }
    public void InterstitialShow(UnityAction interstitialCallback)
    {

        if (IsInterstitialLoaded())
        {
            interstitialedCallback = interstitialCallback;
            this.interstitial.Show();
        }
    }
    #endregion
    #region Reward
    private void RequestRewardAd()
    {
        _showing = false;
        string adUnitId;
#if UNITY_ANDROID
        adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitId = "unexpected_platform";
#endif

        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;//xem het video chua,quan trong nhat
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }
    public void HandleRewardedAdFailedToLoad(object sender, EventArgs args)
    {

    }
    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {

    }
    public void HandleRewardedAdFailedToShow(object sender, EventArgs args)
    {

    }
    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        //xem het quang cao thi moi cho thuc hien goi rewardCallback
        rewardedCallback?.Invoke();
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        //sau khi quang cao tat
        //can request them 1 quang cao moi de hien trong lan tiep theo
        RequestRewardAd();
    }
    public bool IsRewardLoaded()
    {
        return rewardedAd != null && rewardedAd.IsLoaded();
    }
    public void ShowRewardAd(UnityAction rewardCallback)
    {
        if (IsRewardLoaded())//neu quang cao duoc  load thi moi show ra
        {
            this.rewardedCallback = rewardCallback;
            rewardedAd.Show();
        }
    }
    #endregion
    public void DontDestroyBanner()
    {
        this.RequestBanner();
        /*
            +Tuy rang ta da dontdestroyonload  nhung cai quang cao banner do 
             khong phai la child cua adsmanager ma la no dc tao rieng ra
              nen khi loadscecen o dau ta can goi lai ham nay
         */
    }
}
