using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject followp;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        distance = transform.position.x - followp.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float targetObjectX = followp.transform.position.x;
        Vector3 newCameraPosition = transform.position;
        newCameraPosition.x = targetObjectX + distance;
        transform.position = newCameraPosition;
    }
}
