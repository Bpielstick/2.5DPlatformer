using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform pointA, pointB;
    [SerializeField] float speed;
    private Transform destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = pointA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        if (transform.position == pointA.position) { destination = pointB; }
        else if (transform.position == pointB.position) { destination = pointA; }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;      
        }
    }
}
