using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        float rotateSpeed = speed * Time.deltaTime;
        transform.Rotate(0, 0, rotateSpeed);
    }
}
