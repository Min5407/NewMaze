using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject player;
    public GameObject keyImage;


    // Start is called before the first frame update
    void Start()
    {
        keyImage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.5f, 0.7f, 0.5f);



    }
    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            print("hitted");
            //keyImage.SetActive(false);
     
            transform.parent = player.transform;
            this.gameObject.SetActive(false);
            
            //transform.localPosition = new Vector3(0, (float)0.8, 0);



        }
    }

  
}
