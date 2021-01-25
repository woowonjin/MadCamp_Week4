using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControleer : MonoBehaviour
{

    private Transform target;

    [SerializeField] private float smoothSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // Smoothly move Camera
        // first param : current position
        // second param : target position
        // third param : speed
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);
    }
}
