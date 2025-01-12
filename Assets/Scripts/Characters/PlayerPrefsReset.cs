using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
