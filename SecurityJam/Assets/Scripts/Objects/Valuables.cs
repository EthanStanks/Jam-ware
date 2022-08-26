using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuables : MonoBehaviour
{
    private bool broken;
    [SerializeField] SpriteRenderer Renderer;
    [SerializeField] Sprite open, closed;


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
    }

}
