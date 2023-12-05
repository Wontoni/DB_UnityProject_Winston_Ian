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
        var response = JsonUtility.FromJson<RequestFormat>(request.downloadHandler.text);
        print(request.downloadHandler.text);
        print(response.user.is_slime_defeated);

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
        var response =  JsonUtility.FromJson<RequestFormat>(request.downloadHandler.text);
        print(response.success);
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

    private void SuccessfulLogin()
    {

    }

}
