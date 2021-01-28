using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetShopToMain : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
