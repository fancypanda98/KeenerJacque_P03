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

        if (Input.GetKeyDown(KeyCode.Mouse0))
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
                        hold.select = false;
                    }
                    else
                    {
                        send.select = true;
                        hold = send;
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
                                hold.output.source = null;
                            }
                            if (rec.source != hold && rec.source)
                            {
                                rec.source.connect = false;
                                rec.source.output = null;
                            }
                            hold.connect = true;
                            hold.output = rec;
                            rec.source = hold;
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
                                hold.output.source = null;
                                hold.output = null;
                            }
                            hold = null;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 rayDirection = cameraController.transform.forward;
            Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.yellow, 1f);
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance))
            {
                var sendP = objectHit.transform.GetComponent<SendPoint>();
                if (sendP != null)
                {
                    var send = sendP.senderParent;
                    send.activated = !send.activated;
                }
            }
        }
    }
}
