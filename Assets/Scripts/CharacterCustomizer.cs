using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour
{
    public GameObject[] hairs;
    public AvatarSetUp avatar;

    public int hairIndex;
    
    private void Start()
    {
        hairs = avatar.hairStyle;
        avatar.SetAvatar(PlayerData.instance.data);
    }
    
    public void ChangeHair(int increment)
    {
        PlayerData.instance.data.playerHair += increment;
        PlayerData.instance.data.playerHair %= hairs.Length;
        //PlayerPrefs.SetInt("Hair", PlayerData.instance.data.playerHair);
        avatar.SetAvatar(PlayerData.instance.data);
        //PlayerData.instance.SetCostume();
    }
}