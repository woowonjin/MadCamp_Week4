using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Text;
using System;

public class BuyButton : MonoBehaviour
{
    string kind = "";
    string phone = "";
    string url = "";
    //public GameObject bird;

    void Start()
    {
        phone = LoginScene.phone;
    }

    /*Debug.Log(bird);
            
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
            //SceneManager.MoveGameObjectToScene(bird, SceneManager.GetSceneByBuildIndex(2));
            GameObject instance = Instantiate(bird);
    instance.transform.position = new Vector3(0, 0, 1);
    PetManager._Instance.AddGameObject(instance);

            DontDestroyOnLoad(instance);
    //SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    //Application.Quit();*/

    public void BuyPet()
    {   
        Debug.Log(this.gameObject.name);
        if (EventSystem.current.currentSelectedGameObject.name == "Button_bird")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Bird";
           
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Button_cat")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Cat";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_chicken")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Chicken";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_cow")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Cow";
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_dog")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Dog";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_duck")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Duck";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_elephant")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Elephant";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_koala")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Koala";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_llama")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Llama";

        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Button_penguin")
        {
            Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            kind = "Penguin";
        }

        byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);
        byte[] kindBytesForEncoding = Encoding.UTF8.GetBytes(kind);
        string kindEncodedString = Convert.ToBase64String(kindBytesForEncoding);
        url = "http://192.249.18.156:8000/pet/create/?phone=" + phoneEncodedString + "&kind=" + kindEncodedString;
        StartCoroutine(Create());

    }

    IEnumerator Create()
    {
        //List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        //form.Add(new MultipartFormDataSection("phone", "01085844764"));
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        Debug.Log(result);
    }

}
