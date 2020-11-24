using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    [SerializeField] bool toggleActivated = true;
    [SerializeField] Material basic;
    [SerializeField] Material connected;
    [SerializeField] Material hack;
    [SerializeField] Outline outline;
    [SerializeField] GameObject sidePut;
    [SerializeField] string recieverType;
    public GameObject body;
    public bool activated = false;
    public bool connect = false;
    public RecPoint point;


    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        connect = false;
    }

    // Update is called once per frame
    void Update()
    {
        outline.enabled = false;
        var ren = body.GetComponent<Renderer>();
        ren.material = basic;
        /*if (connect)
        {
            ren.material = connected;
        }*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ren.material = hack;
            outline.enabled = true;
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 9f;
            
        }

    }

    public void triggerObject()
    {
        if(recieverType == "light")
        {

            sidePut.SetActive(!sidePut.activeSelf);
        }else if(recieverType == "door")
        {
            activated = !activated;
            if (activated)
            {
                transform.Rotate(0f, 90f, 0f, Space.Self);
            }
            else
            {
                transform.Rotate(0f, -90f, 0f, Space.Self);
            }
        }else if(recieverType == "outlet")
        {
            //play sound
            //play animation
            //deal damage
        }
    }
}
