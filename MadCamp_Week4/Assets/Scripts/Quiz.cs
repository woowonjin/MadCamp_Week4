using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System;


public class Quiz : MonoBehaviour
{
    public Text quest_txt;
    public InputField ans = null;
    string npc;
    string phone;
    string url = "http://192.249.18.156:8000/quest/get/";

    string question = "";
    string answer = "";
    int reward = 0;



    void Start()
    {


        npc = NPCTrigger.npc;
        phone = LoginScene.phone;

        byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);
        byte[] npcBytesForEncoding = Encoding.UTF8.GetBytes(npc);
        string npcEncodedString = Convert.ToBase64String(npcBytesForEncoding);
        url += "?phone=" + phoneEncodedString + "&npc=" + npcEncodedString;
        StartCoroutine(Get());


    }

    IEnumerator Get()
    {
        //List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        //form.Add(new MultipartFormDataSection("phone", "01085844764"));
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        Debug.Log(result);
        byte[] bytesForEncoding = Encoding.UTF8.GetBytes(result);
        string encodedString = Convert.ToBase64String(bytesForEncoding);
        // utf-8 디코딩
        byte[] decodedBytes = Convert.FromBase64String(encodedString);
        string decodedString = Encoding.UTF8.GetString(decodedBytes);
        if (decodedString == "AlreadySolved")
        {
            Debug.Log("This quest already cleared");
            quest_txt.text = "이미 퀘스트를 완료하셨습니다.";
        }
        else
        {

            Quest quest = JsonUtility.FromJson<Quest>(result);
            question = quest.question;
            answer = quest.answer;
            reward = int.Parse(quest.reward);
            quest_txt.text = question;
            Debug.Log(question);
            Debug.Log(answer);
            Debug.Log(reward);
        }
    }

    [Serializable]
    public class Quest
    {
        public string question;
        public string answer;
        public string reward;
    }



    public void Answer()
    {
        if (ans.text.Equals(answer))
        {
            StartCoroutine(Clear());
        }
        else
        {

        }
    }


    IEnumerator Clear()
    {
        string url_clear = "http://192.249.18.156:8000/quest/clear/";
        byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);
        byte[] npcBytesForEncoding = Encoding.UTF8.GetBytes(npc);
        string npcEncodedString = Convert.ToBase64String(npcBytesForEncoding);
        url_clear += "?phone=" + phoneEncodedString + "&npc=" + npcEncodedString;
        UnityWebRequest webRequest = UnityWebRequest.Get(url_clear);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        Debug.Log(result);
        SceneManager.LoadScene("SampleScene");
    }
}
