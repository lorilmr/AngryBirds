using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAsync : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(960,600,false);
        SceneManager.LoadSceneAsync(1);
    }
}
