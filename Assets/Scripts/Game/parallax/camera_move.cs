using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    public float speed = 0.1F;
    public Vector2 direction;
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(direction * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-direction * speed);
        }
    }
}
