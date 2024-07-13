using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField chatInput;
    public ScrollRect chatRect;
    public GameObject messageBox;
    public GameObject content;
    public bool isChatting = false;
    private string messageDraft;

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Slash) && isChatting == false)
        {
            isChatting = true;
            chatInput.text = messageDraft;
            EventSystem.current.SetSelectedGameObject(chatInput.gameObject);
        }

        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && isChatting)
        {
            SendMessage();
            DeselectChat("");
            chatRect.verticalNormalizedPosition = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isChatting)
        {
            DeselectChat(chatInput.text);
        }
    }

    public void DeselectChat()
    {
        DeselectChat(chatInput.text);
    }

    public void DeselectChat(string draft)
    {
        messageDraft = draft;
        chatInput.text = "";
        isChatting = false;

        var eventSystem = EventSystem.current;
        if (!eventSystem.alreadySelecting) eventSystem.SetSelectedGameObject (null);
    }

    public void SendMessage()
    {
        string message = chatInput.text;

        if(message == "")
            return;

        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, (PhotonNetwork.NickName + ": " + message));
    }

    [PunRPC]
    public void GetMessage(string receiveMessage)
    {
        GameObject message = Instantiate(messageBox, Vector3.zero, Quaternion.identity, content.transform);
        message.GetComponent<MessageChat>().messageBox.text = receiveMessage;
        chatInput.text = "";
        messageDraft = "";
        isChatting = false;
    }
}
