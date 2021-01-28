using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : MonoBehaviour
{
    public Transform target;
    public Vector2 direction;
    public float velocity;
    public float default_velocity;
    public float accelaration;
    public Vector2 default_direction;
    private float UPDATE_TIME = 1.0f;
    private float updateTime;
    private float range_x = 0.1f;
    private float range_y = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        updateTime = UPDATE_TIME;
        default_direction.x = Random.Range(-range_x, range_x);
        default_direction.y = Random.Range(-range_y, range_y);
        // 움직일 방향 벡터(자동)
        //default_direction.x = Random.Range(-24.58f, 8.32f);
        //default_direction.y = Random.Range(-14.43f, 30.28f);
        // 가속도

    }

    // Update is called once per frame
    void Update()
    {
        updateTime -= Time.deltaTime;
        if (updateTime > 0)
        {
            //pass
            this.transform.position = new Vector2(transform.position.x + default_direction.x * Time.deltaTime, transform.position.y + default_direction.y * Time.deltaTime);

        }
        else
        {
            default_direction.x = Random.Range(-range_x, range_x);
            default_direction.y = Random.Range(-range_y, range_y);
            //this.transform.position = new Vector2(transform.position.x +  * Time.deltaTime), transform.position.y + ((default_direction.y - transform.position.y) * Time.deltaTime));
            updateTime = UPDATE_TIME;
        }

    }

}
