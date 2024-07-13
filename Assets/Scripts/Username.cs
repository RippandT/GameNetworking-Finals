using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;

public class Username : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TextMeshProUGUI usernamePreview;
    public GameObject errorMessage;
    public GameObject usernameBox;
    public GameObject lobbyBox;

    void Start()
    {
        if(PlayerPrefs.HasKey("Username"))
            usernameField.text = PlayerPrefs.GetString("Username");
    }

    public void SaveUsername()
    {
        string username = usernameField.text;

        if(username.Length == 0)
        {
            errorMessage.SetActive(true);
            return;
        }

        PhotonNetwork.NickName = username;
        usernamePreview.text = username;
        PlayerPrefs.SetString("Username", usernameField.text);
        usernameBox.SetActive(false);
        lobbyBox.SetActive(true);
    }

    public void BackToUsername()
    {
        usernameBox.SetActive(true);
        lobbyBox.SetActive(false);
    }
}
