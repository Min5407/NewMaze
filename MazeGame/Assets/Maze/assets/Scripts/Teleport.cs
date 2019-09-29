using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer.GetComponent<Help>().saveTime();
            SceneManager.LoadScene("BossRoom");
        }
    }
}
