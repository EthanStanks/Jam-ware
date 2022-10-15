using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuables : MonoBehaviour
{
    [SerializeField] public GameObject useGraphic;
    [SerializeField] public string jewelType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D hitter)
    {
        if (hitter.gameObject.tag == "Player")
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = true;
            useGraphic.GetComponent<SpriteMask>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //canUse = false;
            useGraphic.GetComponent<SpriteRenderer>().enabled = false;
            useGraphic.GetComponent<SpriteMask>().enabled = false;
        }
    }

}
