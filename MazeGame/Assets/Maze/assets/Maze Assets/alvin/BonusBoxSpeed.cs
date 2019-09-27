using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBoxSpeed : MonoBehaviour
{
    private GameObject player;
    public int Speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        print(player.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.5f, 0.7f, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           player.SendMessage("AdjustSpeed", Speed);
            Destroy(this.gameObject);
        }
    }
}
