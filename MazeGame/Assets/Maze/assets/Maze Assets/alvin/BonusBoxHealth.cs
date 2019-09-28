using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBoxHealth : MonoBehaviour
{
    private GameObject player;
    public int Health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

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
           player.SendMessage("ApplyDamage", Health);
            Destroy(this.gameObject);
        }
    }
}
