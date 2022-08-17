using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform camTarget;
    [SerializeField] float speed = 1f;

    float moveX;
    float moveY;


    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal") * speed;
        moveY = Input.GetAxisRaw("Vertical") * speed;

        camTarget.position += new Vector3(moveX, 0, moveY);
    }
}
