using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] float shootDistance = 15f;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Camera cameraController;
    [SerializeField] Color skyNormal;
    [SerializeField] Color skyHack;
    [SerializeField] Transform rayOrigin;
    [SerializeField] LayerMask hitLayers;

    public Transform playerBody;

    Sender hold = null;
    float xRotation = 0f;
    RaycastHit objectHit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 rayDirection = cameraController.transform.forward;
            Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.white, 1f);
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
            {
                Debug.Log(objectHit.transform.name);

                var sendP = objectHit.transform.GetComponent<SendPoint>();
                if (sendP != null)
                {
                    var send = sendP.senderParent;
                    if (hold != null)
                    {
                        hold.connect = false;
                        hold.select = false;
                        hold = send;
                        hold.select = true;
                        hold.connect = false;
                        if (hold.output != null)
                        {
                            hold.connect = false;
                            hold.output = null;
                        }
                    }
                    else
                    {
                        hold = send;
                        hold.select = true;
                        if (hold.output != null)
                        {
                            hold.connect = false;
                            hold.output = null;
                        }
                    }
                }
                else
                {
                    var recP = objectHit.transform.GetComponent<RecPoint>();
                    if (recP != null)
                    {
                        var rec = recP.recieverParent;
                        if (hold != null)
                        {
                            if (hold.output != null)
                            {
                                hold.output.connect = false;
                            }
                            hold.connect = true;
                            hold.output = rec;
                            hold.select = false;
                            rec.connect = true;
                            hold = null;
                        }
                    }
                    else
                    {
                        if (hold != null)
                        {
                            hold.select = false;
                            hold.connect = false;
                            if (hold.output != null)
                            {
                                hold.output.connect = false;
                                hold.output = null;
                            }
                            hold = null;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 rayDirection = cameraController.transform.forward;
            Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
            {
                var sendP = objectHit.transform.GetComponent<SendPoint>();
                if (sendP != null)
                {
                    var send = sendP.senderParent;
                    if (send.output != null)
                    {
                        send.output.triggerObject();
                    }
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            cameraController.backgroundColor = skyHack;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            cameraController.backgroundColor = skyNormal;
        }


    }
}
