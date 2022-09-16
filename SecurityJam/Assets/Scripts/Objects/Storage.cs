using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public bool broken;
    [SerializeField] public SpriteRenderer Renderer;
    [SerializeField] public Sprite open, closed;
    [SerializeField] public GameObject jewel;
    [SerializeField] public GameObject useGraphic;
    [SerializeField] public string jewelType;


    // Start is called before the first frame update
    void Start()
    {
        broken = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void openSafe()
    {
        Renderer.sprite = open;
    }

    void closeSafe()
    {
        Renderer.sprite = closed;
    }

    private void OnTriggerEnter2D(Collider2D hitter)
    {
        if (hitter.gameObject.CompareTag("Robber") && broken == false)
        {
            openSafe();
            broken = true;
        }
        if (hitter.gameObject.tag == "Player" && broken == true && hitter.GetComponent<PlayerManager>().heldObject == jewel)
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //canUse = false;
            useGraphic.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
