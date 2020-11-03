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

    Reciever hold = null;
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

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 rayDirection = cameraController.transform.forward;
            Debug.DrawRay(rayOrigin.position, rayDirection * shootDistance, Color.white, 1f);
            if (Physics.Raycast(rayOrigin.position, rayDirection, out objectHit, shootDistance, hitLayers))
            {
                var send = objectHit.transform.GetComponent<Sender>();
                if (send != null)
                {
                    
                }
            }
        }

    }
}
