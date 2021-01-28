using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewUserToLogin : MonoBehaviour
{
    public InputField new_phone_num;
    public InputField new_password;

    public void SubmitButton()
    {
        if (new_phone_num.text.Length.Equals(11))
        {
            if (new_password.text.Length >= 4 )
            {
                SceneManager.LoadScene("LoginScene");
            }
        }
    }
}
