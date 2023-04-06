using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : MonoBehaviour
{
    private Vector3 direction;

    void Update()
    {
        direction = transform.forward;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}