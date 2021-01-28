using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainToMarket : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Market");
    }
}
