using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 180f;
    public float touchSpeed = 15f;
    public Touch theTouch;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.forward, turnSpeed * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Began)
            {
                // start phase
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
            {
                transform.Rotate(Vector3.forward, touchSpeed * theTouch.deltaPosition.x * Time.deltaTime);

            }
        }
    }
}
