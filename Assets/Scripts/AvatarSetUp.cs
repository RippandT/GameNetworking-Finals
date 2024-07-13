using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AvatarSetUp : MonoBehaviour
{
    private PhotonView myPV;
    public GameObject[] hairStyle;
    public int hairIndex;
    public int CurrentHairIndex
    {
        get { return hairIndex; }
        set
        {
            if (value >= 0 && value < hairStyle.Length)
            {
                hairIndex = value;
                //HairUpdater();
            }
        }
    }

    void Start()
    {
        CurrentHairIndex = PlayerData.instance.data.playerHair;
        myPV = PhotonView.Get(this);
        //HairUpdater();
    }

    public void HairUpdater()
    {
        for (int i = 0; i < hairStyle.Length; i++)
        {
            hairStyle[i].SetActive(i == CurrentHairIndex);
        }
        hairIndex = CurrentHairIndex;
    }

    public void SetAvatar(Data avatarData)
    {
        CurrentHairIndex = avatarData.playerHair;
        HairUpdater();
        //hairStyle[avatarData.playerHair].SetActive(true);
    }
}
