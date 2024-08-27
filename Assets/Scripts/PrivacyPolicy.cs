using System.Collections;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    [SerializeField] private UniWebView webView;
    [SerializeField] private string policyUrl;
    [SerializeField] private GameObject loadingScreen, canvas;
    [SerializeField] private GameObject policyBackground, alternateBackground;

    private bool hasPolicyLoaded = false;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        CheckInternetConnection();
    }

    private void CheckInternetConnection()
    {
        if (IsInternetConnected())
        {
            HandlePolicyDisplay();
        }
        else
        {
            ShowOfflineMode();
            StartCoroutine(RetryConnectionUntilSuccessful());
        }
    }

    private bool IsInternetConnected()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    private void ShowOfflineMode()
    {
        loadingScreen.SetActive(false);
        canvas.SetActive(false);
        PlayerPrefs.SetString("PolicyStatus", "Accepted");
        ApplyOfflineSettings();
    }

    private IEnumerator RetryConnectionUntilSuccessful()
    {
        while (!IsInternetConnected())
        {
            ShowOfflineMode();
            yield return new WaitForSeconds(5f);
        }

        LoadPolicyPage();
    }

    private void LoadPolicyPage()
    {
        canvas.SetActive(true);
        loadingScreen.SetActive(true);
        webView.OnPageFinished += HandlePolicyPageLoad;
        InitializeWebView();
        webView.Load(policyUrl);
    }

    private void HandlePolicyPageLoad(UniWebView webView, int statusCode, string loadedUrl)
    {
        if (hasPolicyLoaded)
            return;

        hasPolicyLoaded = true;

        bool isPolicyPage = loadedUrl == policyUrl;
        GameObject backgroundToShow = isPolicyPage ? policyBackground : alternateBackground;
        backgroundToShow.SetActive(true);

        if (isPolicyPage)
        {
            PlayerPrefs.SetString("PolicyStatus", "Accepted");
            webView.Show();
        }
        else
        {
            Destroy(gameObject);
        }

        LogLoadDetails();
    }

    public void AcceptPolicy()
    {
        HandlePolicyDisplay();
    }

    private void HandlePolicyDisplay()
    {
        string policyStatus = PlayerPrefs.GetString("PolicyStatus", "");

        if (string.IsNullOrEmpty(policyStatus))
        {
            StartCoroutine(RetryConnectionUntilSuccessful());
            PostAcceptanceOperations();
        }
        else if (policyStatus == "Accepted")
        {
            canvas.SetActive(false);
            webView.Hide();
        }
        else
        {
            DisplayPreviousPolicyPage(policyStatus);
        }

        PerformPolicyActions();
    }

    private void DisplayPreviousPolicyPage(string url)
    {
        webView.Load(url);
        webView.Show();
        alternateBackground.SetActive(true);
        PostLoadOperations();
    }

    public void GoBack() => webView.GoBack();
    public void GoForward() => webView.GoForward();

    private void InitializeWebView()
    {
        // Placeholder for webView initialization logic if needed
    }

    private void ApplyOfflineSettings()
    {
        // Placeholder for offline settings logic if needed
    }

    private void LogLoadDetails()
    {
        Debug.Log("Page load details logged.");
    }

    private void PostAcceptanceOperations()
    {
        Debug.Log("Post-acceptance operations performed.");
    }

    private void PerformPolicyActions()
    {
        Debug.Log("Policy actions performed.");
    }

    private void PostLoadOperations()
    {
        Debug.Log("Post-load operations performed.");
    }
}
