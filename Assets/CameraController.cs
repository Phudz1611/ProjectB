using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetObject;
    public Vector3 camera_offset;
    public float smoothFactor = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        camera_offset = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = targetObject.transform.position + camera_offset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);
        transform.LookAt(targetObject);
    }
}
