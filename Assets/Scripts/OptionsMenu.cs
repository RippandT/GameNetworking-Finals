using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }
}
