using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform camTarget;
    [SerializeField] float speed = 5;

    float moveX;
    float moveY;

    bool paused = false;

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal") * speed;
        moveY = Input.GetAxisRaw("Vertical") * speed;

        camTarget.position += new Vector3(moveX, 0, moveY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (paused)
            {
                Time.timeScale = 1;
            }
            else if (!paused)
            {
                Time.timeScale = 0;
            }
            paused = !paused;
        }
    }
}
