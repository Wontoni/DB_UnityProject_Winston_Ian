using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    [SerializeField] GameObject registerScreen;
    [SerializeField] GameObject loginScreen;

    [SerializeField] TMP_InputField registerUsername;
    [SerializeField] TMP_InputField registerPassword;
    [SerializeField] TMP_InputField registerEmail;

    [SerializeField] TMP_InputField loginEmail;
    [SerializeField] TMP_InputField loginPassword;

    public void SwitchLoginScreen()
    {
        registerScreen.SetActive(!registerScreen.activeInHierarchy);
        loginScreen.SetActive(!registerScreen.activeInHierarchy);
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
        string url = "http://localhost:3000/auth/register";
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
        string url = "http://localhost:3000/auth/login";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        print(response);
    }

    public void CheckSession()
    {
        StartCoroutine(Session());
    }

    public IEnumerator Session()
    {
        string url = "http://localhost:3000/verify";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        print(response);
    }

    public void DestroySession()
    {
        StartCoroutine(Destroy());
    }

    public IEnumerator Destroy()
    {
        WWWForm form = new();
        string url = "http://localhost:3000/auth/logout";
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();
        var response = request.downloadHandler.text;
        print(response);
    }

}
