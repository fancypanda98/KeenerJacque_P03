using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : MonoBehaviour
{
    public Reciever output;
    [SerializeField] bool toggleActivated = true;
    [SerializeField] Material basic;
    [SerializeField] Material selected;
    [SerializeField] Material connected;
    [SerializeField] Material hack;
    [SerializeField] Outline outline;
    [SerializeField] GameObject sidePut;
    [SerializeField] LineRenderer line;
    [SerializeField] Camera look;
    [SerializeField] Transform rayOrigin;
    public GameObject body;
    public bool activated = false;
    public bool select = false;
    public bool connect = false;


    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        select = false;
        connect = false;
    }

    // Update is called once per frame
    void Update()
    {
        line.enabled = false;
        outline.enabled = false;
        var ren = body.GetComponent<Renderer>();
        ren.material = basic;
        /*if (connect)
        {
            ren.material = connected;
        }
        if (select)
        {
            ren.material = selected;
        }*/

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ren.material = hack;
            outline.enabled = true;
        }

        sidePut.SetActive(activated);

        if (select)
        {
            line.enabled = true;
            line.SetPosition(0, body.transform.position);
            line.SetPosition(1, rayOrigin.position + look.transform.forward * 15);
        }
        if (connect)
        {
            line.enabled = true;
            line.SetPosition(0, body.transform.position);
            line.SetPosition(1, output.body.transform.position);
        }
    }
}
