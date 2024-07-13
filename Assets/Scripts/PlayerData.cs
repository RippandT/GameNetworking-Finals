using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public Data data;
    public CharacterCustomizer customizer;

    private void OnEnable()
    {
        data = new Data();
        if (instance == null)
        {
            PlayerData.instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        //Debug.Log(instance.data.playerHair);
    }

    public string AvatarToString()
    {
        string returnString = JsonUtility.ToJson(PlayerData.instance.data);
        return returnString;
    }

    public void SetCostume()
    {
        PlayerData.instance.data.playerHair = customizer.hairIndex;
    }
}

public class Data
{
    public static PlayerData instance;
    public int playerHair;
}