using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System;

public class LoginScene : MonoBehaviour
{
    public static string phone = "";

    public InputField phone_num = null;
    public InputField password = null;

    string url = "http://192.249.18.156:8000/user/login/";

    // Start is called before the first frame update
    void Start()
    {
        /*byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);

        byte[] pwdBytesForEncoding = Encoding.UTF8.GetBytes(pwd);
        string pwdEncodedString = Convert.ToBase64String(pwdBytesForEncoding);
        */
        /*url += "?phone=" + phoneEncodedString + "&pwd=" + pwdEncodedString;
        StartCoroutine(Login());*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* IEnumerator DataGet()
     {

     }*/

    IEnumerator Login()
    {
        //List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        //form.Add(new MultipartFormDataSection("phone", "01085844764"));


        byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone_num.text);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);

        byte[] pwdBytesForEncoding = Encoding.UTF8.GetBytes(password.text);
        string pwdEncodedString = Convert.ToBase64String(pwdBytesForEncoding);

        url += "?phone=" + phoneEncodedString + "&pwd=" + pwdEncodedString;

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;

        Debug.Log(result);

        byte[] bytesForEncoding = Encoding.UTF8.GetBytes(result);
        string encodedString = Convert.ToBase64String(bytesForEncoding);

        // utf-8 디코딩
        byte[] decodedBytes = Convert.FromBase64String(encodedString);
        string decodedString = Encoding.UTF8.GetString(decodedBytes);

        if (result == "LoginSuccess")
        {
            Debug.Log("Login Success");
            phone = phone_num.text;
            SceneManager.LoadScene("SampleScene");
        }
        else if (result == "IdError")
        {
            Debug.Log("Id Error");
        }
        else if (result == "PasswordError")
        {
            Debug.Log("Password Error");
        }
    }



    public void LoginButton()
    {
        StartCoroutine(Login());
    }
    
}
