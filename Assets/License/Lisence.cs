using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lisence : MonoBehaviour
{
    [SerializeField] private InputField UniqueIdText;
    [SerializeField] private InputField LicenseText;
    [SerializeField] private GameObject LicensePanel;

    private string key = "BubaLogoped";

    string sysInfo;

    void Start()
    {

        sysInfo = SystemInfo.deviceUniqueIdentifier;
        CheckLicense();

        UniqueIdText.text = sysInfo;
    }


    public void CopyLicense()
    {
        GUIUtility.systemCopyBuffer = UniqueIdText.text;
    }

    public void PasteLicense()
    {
        LicenseText.text = GUIUtility.systemCopyBuffer;

    }

    public void CheckLicense()
    {
        string inputPass = LicenseText.text;
        inputPass = Regex.Replace(inputPass, @"[^0-9a-zA-Z]+", "");

        string truePas = XorEncrypt(sysInfo, key);
        truePas = Regex.Replace(truePas, @"[^0-9a-zA-Z]+", "");

        if (PlayerPrefs.HasKey("SavedString") && PlayerPrefs.GetString("SavedString") == truePas)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayerPrefs.DeleteAll();
            LicensePanel.SetActive(true);
            Screen.fullScreen = false;

        }


        if (truePas == inputPass)
        {
            PlayerPrefs.SetString("SavedString", inputPass);

            LicensePanel.SetActive(false);
            Screen.fullScreen = true;

            SceneManager.LoadScene(1);
        }
    }


    private string XorEncrypt(string input, string key)
    {
        char[] data = input.ToCharArray();
        char[] keyData = key.ToCharArray();
        char[] result = new char[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (char)(data[i] ^ keyData[i % keyData.Length]);
        }
        return new string(result);
    }
}
