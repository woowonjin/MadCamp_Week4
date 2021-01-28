using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCTrigger : MonoBehaviour
{
    public static string npc = "";
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.tag == "Player")
    //    {
    //        Application.LoadLevel("PetShop");
    //    }
    //}

    void OnTriggerEnter2D()
    {
        if (this.gameObject.name == "ryu")
        {
            Debug.Log(this.gameObject.name);
            //Application.LoadLevel("PetShop");
            SceneManager.LoadScene("PetShop", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("here");
            Debug.Log(this.gameObject.name);
            switch (this.gameObject.name)
            {
                case "Chan":
                    npc = "Chan";
                    Application.LoadLevel("Quest");
                    break;

                case "Ho":
                    npc = "Ho";
                    Application.LoadLevel("Quest");
                    break;

                case "Hae":
                    npc = "Hae";
                    Application.LoadLevel("Quest");
                    break;

                case "Kang":
                    npc = "Kang";
                    Application.LoadLevel("Quest");
                    break;

                case "Yoon":
                    npc = "Yoon";
                    Application.LoadLevel("Quest");
                    break;

            }
        }
    }
}
