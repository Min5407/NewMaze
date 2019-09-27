﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{

    private Vector3 initialPos;
    private Vector3 movePos;
    private float totalDist;
    // Start is called before the first frame update
    private bool doortrue = false;
    public float Xvalue = 0;
    public float Yvalue = 0;
    public float Zvalue = 0;


    void Start()
    {
        initialPos = transform.position;
        movePos = new Vector3(transform.position.x + Xvalue, transform.position.y + Yvalue, transform.position.z + Zvalue);
        //movePos = moveToObject.transform.position;
        totalDist = Vector3.Distance(initialPos, movePos);
    }

    // Update is called once per frame
    void Update()
    {
        if (doortrue)
        {
            Open();
        }
    }

    public void Open()
    {
        doortrue = true;
        float distCovered = (Vector3.Distance(transform.position, initialPos) + 4.0f * Time.deltaTime) / totalDist;
        GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(initialPos, movePos, distCovered));
    }
}
