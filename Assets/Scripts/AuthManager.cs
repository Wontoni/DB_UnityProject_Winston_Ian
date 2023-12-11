using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    [SerializeField] public GameObject gameManager;
    [SerializeField] public GameObject registerScreen;
    [SerializeField] public GameObject loginScreen;
    [SerializeField] public GameObject titleScreen;

    [SerializeField] public TMP_InputField registerUsername;
    [SerializeField] public TMP_InputField registerPassword;
    [SerializeField] public TMP_InputField registerEmail;

    [SerializeField] public TMP_InputField loginEmail;
    [SerializeField] public TMP_InputField loginPassword;


    public void OpenRegister()
    {
        registerScreen.SetActive(true);
        loginScreen.SetActive(false);
        titleScreen.SetActive(false);
    }

    public void OpenLogin()
    {
        registerScreen.SetActive(false);
        loginScreen.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void RegisterPress()
    {
        StartCoroutine(RegisterUser());
    }

    public void LoginPress()
    {
        StartCoroutine(LoginUser());
    }

    public IEnumerator RegisterUser()
    {
        WWWForm form = new();
        form.AddField("username", registerUsername.text);
        form.AddField("password", registerPassword.text);
        form.AddField("email", registerEmail.text);
        string url = "https://unity-backend.onrender.com/auth/register";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        print(response);
    }

    public IEnumerator LoginUser()
    {
        WWWForm form = new();
        form.AddField("email", loginEmail.text);
        form.AddField("password", loginPassword.text);
        string url = "https://unity-backend.onrender.com/auth/login";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = JsonUtility.FromJson<RequestFormat>(request.downloadHandler.text);
        if(response.success)
        {
            SuccessfulLogin();
        }
    }

    public void CheckSession()
    {
        StartCoroutine(Session());
    }

    public IEnumerator Session()
    {
        string url = "https://unity-backend.onrender.com/verify";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        var response =  JsonUtility.FromJson<RequestFormat>(request.downloadHandler.text);
    }

    public void DestroySession()
    {
        StartCoroutine(Destroy());
    }

    public IEnumerator Destroy()
    {
        WWWForm form = new();
        string url = "https://unity-backend.onrender.com/auth/logout";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        print(response);
    }

    private void SuccessfulLogin()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
