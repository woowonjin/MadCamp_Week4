using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class location : MonoBehaviour
{
    private Rigidbody2D rb;
    bool gpsInit = false;
    LocationInfo currentGPSPosition;
    int gps_connect = 0;
    float detailed_num = 1.0f;//기존 좌표는 float형으로 소수점 자리가 비교적 자세히 출력되는 double을 곱하여 자세한 값을 구합니다.
    public float cur_latitude;
    public float cur_longitude;
    public int num_refresh;

    private float initLat = 36.368169f;
    private float initLng = 127.363846f;

    private float initX = -4.9f;
    private float initY = 1.0f;

    private float perLat = 2851.253867f;
    private float perLng = 2101.14476f;

    private float moveH, moveV;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Input.location.Start(0.5f);
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
    void RetrieveGPSData()
    {
        currentGPSPosition = Input.location.lastData;//gps를 데이터를 받습니다.
        cur_latitude = currentGPSPosition.latitude * detailed_num; //위도 값을 받아
        cur_longitude = currentGPSPosition.longitude * detailed_num; //경도 값을 받아
        gps_connect++;
        num_refresh = gps_connect;
    }
    void FixedUpdate()
    {
        moveV = (cur_latitude - initLat) * perLat + initY;
        moveH = (cur_longitude - initLng) * perLng + initX;

        Debug.Log("moveV : " + moveV + ", moveH : " + moveH);
        Vector2 temp = new Vector2(moveH, moveV);
        rb.MovePosition(temp);
        //rb.velocity = new Vector2(moveH, moveV); // OPTIONAL rb.MovePosition();
        //rb.position = temp;
        //Vector2 direction = new Vector2(moveH, moveV);
        //FindObjectOfType<PlayerAnimation>().SetDirection(direction);
    }
}