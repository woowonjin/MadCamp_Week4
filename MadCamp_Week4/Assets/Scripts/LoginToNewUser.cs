using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginToNewUser : MonoBehaviour
{
    public void NewUser()
    {
        SceneManager.LoadScene("NewUser");
    }
}
