using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMeta : MonoBehaviour
{
    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }

    public void BackToLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}
