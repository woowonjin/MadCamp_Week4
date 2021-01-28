using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Text;


public class location : MonoBehaviour
{

    List<string> myPets = new List<string>();
    string phone = ""; //get from front
    string url = "http://192.249.18.156:8000/pet/get/";

    public GameObject Bird;
    public GameObject Cat;
    public GameObject Chicken;
    public GameObject Cow;
    public GameObject Dog;
    public GameObject Duck;
    public GameObject Elephant;
    public GameObject Koala;
    public GameObject Llama;
    public GameObject Penguin;


    static float UPDATE_TIME = 1f;
    float updateTime = UPDATE_TIME;
    private Rigidbody2D rb;
    bool gpsInit = false;
    LocationInfo currentGPSPosition;
    int gps_connect = 0;
    float detailed_num = 1.0f;//기존 좌표는 float형으로 소수점 자리가 비교적 자세히 출력되는 double을 곱하여 자세한 값을 구합니다.
    public int num_refresh;
    //처음 값
    private float initLat = 36.368169f;
    private float initLng = 127.363846f;
    private float initX = -4.9f;
    private float initY = 1.0f;
    //현재 값
    private float cur_X;
    private float cur_Y;
    //움직일 위치값

    float random_x;
    float random_y;

    private float target_X, target_Y;
    private float target_latitude, target_longitude;
    //위도, 경도 1당 위치값
    private float perLat = 2851.253867f;
    private float perLng = 2101.14476f;
    private static float speed = 1.0f;
    //location 임시값
    private float temp_latitude, temp_longitude;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        phone = LoginScene.phone;
        byte[] phoneBytesForEncoding = Encoding.UTF8.GetBytes(phone);
        string phoneEncodedString = Convert.ToBase64String(phoneBytesForEncoding);
        
        url += "?phone=" + phoneEncodedString;
        StartCoroutine(Get());


        rb = GetComponent<Rigidbody2D>();
        Input.location.Start();
        //캐릭터를 기준점으로 이동
        rb.MovePosition(new Vector2(initX, initY));
        cur_X = initX;
        cur_Y = initY;
        target_X = initX;
        target_Y = initY;
        target_latitude = initLat;
        target_longitude = initLng;
        //36.374532, 127.364784
        //test
        /*target_latitude = 36.374532f;
        target_longitude = 127.364784f;
        gps_connect = 1;
        target_X = (target_longitude - initLng) * perLng + initX;  //target 좌표
        target_Y = (target_latitude - initLat) * perLat + initY;   //target 좌표*/
        //rb.MovePosition(new Vector2(target_X, target_Y));
        int wait = 1000; // 기본 값
        // Checks if the GPS is enabled by the user (-> Allow location )
        if (Input.location.isEnabledByUser)//사용자에 의하여 좌표값을 실행 할 수 있을 경우
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)//초기화 진행중이면
            {
                wait--; // 기다리는 시간을 뺀다
            }
            //GPS를 잡는 대기시간
            if (Input.location.status != LocationServiceStatus.Failed)//GPS가 실행중이라면
            {
                gpsInit = true;
                // We start the timer to check each tick (every 3 sec) the current gps position
                InvokeRepeating("RetrieveGPSData", 0.0001f, 1.0f);//0.0001초에 실행하고 0.3초마다 해당 함수를 실행합니다.
            }
        }
        else//GPS가 없는 경우 (GPS가 없는 기기거나 안드로이드 GPS를 설정 하지 않았을 경우
        {
        }
    }

    IEnumerator Get()
    {
        //List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        //form.Add(new MultipartFormDataSection("phone", "01085844764"));
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        string result = webRequest.downloadHandler.text;
        string result_full = "{\"Items\":" + result + "}";
        Debug.Log(result_full);
        Pet[] pets = JsonHelper.FromJson<Pet>(result_full);
        foreach (Pet pet in pets)
        {
            myPets.Add(pet.kind);
        }

        foreach(string kind in myPets)
        {   //kind
            
            random_x = UnityEngine.Random.Range(-21.53f, 1.92f);
            random_y = UnityEngine.Random.Range(-4.55f, 20.35f);
            Vector3 pos = new Vector3(random_x, random_y, -5);
            if(kind == "BIRD")
            {
                Instantiate(Bird, pos, Quaternion.identity);
            }
            else if(kind == "CAT")
            {
                Instantiate(Cat, pos, Quaternion.identity);
            }
            else if (kind == "CHICKEN")
            {
                Instantiate(Chicken, pos, Quaternion.identity);
            }
            else if (kind == "COW")
            {
                Instantiate(Cow, pos, Quaternion.identity);
            }
            else if (kind == "DOG")
            {
                Instantiate(Dog, pos, Quaternion.identity);
            }
            else if (kind == "DUCK")
            {
                Instantiate(Duck, pos, Quaternion.identity);
            }
            else if (kind == "ELEPHANT")
            {
                Instantiate(Elephant, pos, Quaternion.identity);
            }
            else if (kind == "KOALA")
            {
                Instantiate(Koala, pos, Quaternion.identity);
            }
            else if (kind == "LLAMA")
            {
                Instantiate(Llama, pos, Quaternion.identity);
            }
            else if (kind == "PENGUIN")
            {
                Instantiate(Penguin, pos, Quaternion.identity);
            }
            //GameObject prefab = Resources.Load("Prefab/Bird") as GameObject;
            //GameObject pet = MonoBehaviour.Instantiate(prefab) as GameObject;
        }

    }


    void RetrieveGPSData()
    {
        Debug.Log("RetrieveGPSData");
        if (updateTime > 0)
        {
            // nothing
        }
        else
        {
            if (Input.location.status != LocationServiceStatus.Failed)
            {
                currentGPSPosition = Input.location.lastData;//gps를 데이터를 받습니다.
                cur_X = target_X;
                cur_Y = target_Y;
                target_latitude = currentGPSPosition.latitude * detailed_num; //위도 값을 받아
                target_longitude = currentGPSPosition.longitude * detailed_num; //경도 값을 받아
                target_X = (target_longitude - initLng) * perLng + initX;  //target 좌표
                target_Y = (target_latitude - initLat) * perLat + initY;   //target 좌표
                if (gps_connect == 0)
                {
                    rb.MovePosition(new Vector2(target_X, target_Y));
                    cur_X = target_X;
                    cur_Y = target_Y;
                    gps_connect++;
                }
                updateTime = UPDATE_TIME;
            }
        }
    }
    //target_X = (target_longitude - initLng) * perLng + initX;  //target 좌표
    //target_Y = (target_latitude - initLat) * perLat + initY;   //target 좌표
    void FixedUpdate()
    {
    }
    void Update()
    {
        //이동해야함
        updateTime -= Time.deltaTime;
        if (gps_connect > 0)
        {
            Vector2 position = rb.position;
            if (position.x <= target_X + 0.00001 && position.x >= target_X - 0.00001 && position.y <= target_Y + 0.00001 && position.y >= target_Y - 0.00001)
            {
                // Stop
               
            }
            else
            {
                
                float moveX = (target_X - position.x) * Time.deltaTime + position.x;
                float moveY = (target_Y - position.y) + Time.deltaTime + position.y;
                
                Vector2 dir = new Vector2((target_X - position.x), (target_Y - position.y));
                Vector2 move = new Vector2(moveX, moveY);
                FindObjectOfType<animationlocation>().SetDirection(dir);
                rb.MovePosition(move);
            }
        }
       
    }
}

[Serializable]
public class Pet
{
    public string kind;
}
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }
    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }
    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}