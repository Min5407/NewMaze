using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private const int V = 0;
    public GameObject player;
    public GameObject keyImage;
    GameObject FoodCanvas;
    public List<GameObject> FoodImages;

    // Start is called before the first frame update
    void Start()
    {
        keyImage.SetActive(false);
        FoodCanvas = GameObject.Find("ChickenHealth");
        foodImages();


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

            for (int i = 0; i < FoodImages.Count; i++)
            {
              
                if (FoodImages[i].name == this.gameObject.name)
                {
                    FoodImages[i].SetActive(true);
                }
            }
            //keyImage.SetActive(false);

            transform.parent = player.transform;
            this.gameObject.SetActive(false);
            
            //transform.localPosition = new Vector3(0, (float)0.8, 0);



        }
    }
    void foodImages()
    {
        foreach (Transform child in FoodCanvas.transform)
        {
            if (child.tag == "Food")
            {
                FoodImages.Add(child.gameObject);
            }
        }
    }

  


}
