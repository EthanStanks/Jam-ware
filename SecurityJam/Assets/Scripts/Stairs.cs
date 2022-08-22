using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    private bool canUse = false;
    [SerializeField] private GameObject stairsObj;
    [SerializeField] public bool goesUp = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canUse = false;
        }
    }

    /*
    void useStairs(GameObject thingy)
    {
        if (thingy.tag == "Player" && canUse == true)
        {
            if (thingy.transform.position.y > stairsObj.transform.position.y)
            {
                thingy.transform.position.y = thingy.transform.position.y - (2.0f * stairsObj.transform.localScale.y);
            }
            if (thingy.transform.position.y < stairsObj.transform.position.y)
            {

            }
        }
    }
     */
}
