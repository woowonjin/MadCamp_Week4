using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class animationlocation : MonoBehaviour
{
    private Animator anim;

    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    int lastDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1 " + result1);

        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        Debug.Log("R2 " + result2);

        float result3 = Vector2.SignedAngle(Vector2.up, Vector2.down);
        Debug.Log("R3 " + result3);
    }

    // each direction will match with one string element
    // We used direction to determine their animation
    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;
        if (_direction.magnitude < 0.01) // Character is static. & velocity is close to zero
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;

            lastDirection = DirectionToIndex(_direction); // Get the index of the slice from the direction vector
        }

        anim.Play(directionArray[lastDirection]);
    }

    // Converts a Vector2 direction to an index to a slice around a circle
    // counter-clock direction
    private int DirectionToIndex(Vector2 _direction)
    {
        //When normalized, a vector keeps the same direction but its length is 1.0

        Vector2 norDir = _direction.normalized; // return this vector with a magnitude of 1 and get the normalized to an index

        float step = 360 / 8; // 45 one circle and 8 slices // Calculate how many degrees one slice is 
        float offset = step / 2; // 22.5 // OFFSET help us easy to calculate and get the correct index of the string array

        float angle = Vector2.SignedAngle(Vector2.up, norDir); // returns the signed angle in degrees between A and B

        angle += offset; // Hlep us easy to calculate and get the correct index of the string array
        if (angle < 0) // avoid the negative number
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
}