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
    [SerializeField] GameObject sidePut;
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
        var ren = body.GetComponent<Renderer>();
        ren.material = basic;
        if (connect)
        {
            ren.material = connected;
        }
        if (select)
        {
            ren.material = selected;
        }
        sidePut.SetActive(activated);
    }
}
