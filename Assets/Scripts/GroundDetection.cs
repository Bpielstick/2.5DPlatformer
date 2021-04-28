using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (!characterController.isGrounded)
        {
            RaycastHit Ground;
            RaycastHit InFront;
            RaycastHit Behind;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.3f, Color.yellow);           
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out Ground, 1.3f))
            {
                Debug.Log("raycast hit");
                jumpEnabled = true;
            }
            else
            {
                Debug.DrawRay(transform.position, new Vector3(.5f, -1.3f, 0) * 1, Color.yellow);
                if (Physics.Raycast(transform.position, new Vector3(-.5f, -1.3f, 0), out Ground, 1))
                {
                    jumpEnabled = true;

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 1f, Color.red);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out Behind, 1))
                    {
                        if (Ground.Equals(Behind))
                        {
                            jumpEnabled = false;
                        }
                    }
                }

                Debug.DrawRay(transform.position, new Vector3(-.5f, -1.3f, 0) * 1, Color.yellow);
                if (Physics.Raycast(transform.position, new Vector3(.5f, -1.3f, 0), out Ground, 1))
                {
                    jumpEnabled = true;

                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 1f, Color.red);
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward) * 1f, out InFront, 1))
                    {
                        if (Ground.Equals(InFront))
                        {
                            jumpEnabled = false;
                        }
                    }
                }
            }            
        }
        */

    }
}
