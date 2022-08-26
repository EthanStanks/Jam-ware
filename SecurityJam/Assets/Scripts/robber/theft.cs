using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class theft : MonoBehaviour
{

    bool walkingRight, walkingLeft; 
    public bool usedStairs = false;
    private bool hasGem = false;
    int temp = 0;
    [SerializeField] private GameObject robberObj;
    //[SerializeField] private Animator robberAnimator;
    [SerializeField] private float robbingSpeed;
    internal Collider2D _collider;
    [SerializeField] private GameObject nothing;
    [SerializeField] private GameObject full;
    [SerializeField] public bool isCaught; // this will be set to true if the robber is in the player flashlight
    // Start is called before the first frame update
    void Start()
    {
        walkinAround();
        nothing.SetActive(true);
        full.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingRight)
        {
            walkRight();
        }
        else if (walkingLeft)
        {
            walkLeft();
        }
        if (usedStairs)
        {
            temp++;
            if (temp == 50){ usedStairs = false; temp = 0; }
        }
        if(isCaught)
        {
            Debug.Log("I've been beated by a big flashlight");
        }
    }

    private void OnTriggerEnter2D(Collider2D hitter)
    {
        if (hitter.gameObject.CompareTag("stairs"))
        {
            considerStairs(hitter.gameObject);
        }
        else if (hitter.gameObject.CompareTag("Valuables"))
        {
            if (hasGem == false)
            {
                hasGem = true;
                nothing.SetActive(false);
                full.SetActive(true);
            }
        }
        else 
        {
            walkingLeft = !walkingLeft;
            walkingRight = !walkingRight;
        }
    }

    string coinFlip()
    {
        int random2 = Random.Range(0,10);

        if (random2%2 == 0)
        {
            return "heads";
        }
        else if (random2%2 == 1)
        {
            return "tails";
        }
        else
        {
            return "itsbroke";
        }
    }

    void walkRight()
    {
        robberObj.transform.position += Vector3.right * robbingSpeed * Time.deltaTime;
    }

    void walkLeft()
    {
        robberObj.transform.position += Vector3.right * -robbingSpeed * Time.deltaTime;
    }

    void walkinAround()
    {
        string choice = coinFlip();
        if (choice == "heads")
        {
            walkingRight = true; walkingLeft = false;
            //robberAnimator.SetBool("isWalking", true);
        }
        else if (choice == "tails")
        {
            walkingLeft = true; walkingRight = false;
            //robberAnimator.SetBool("isWalking", true);
        }
    }

    void considerStairs(GameObject stairs)
    {
        string choice = coinFlip();
        if (choice == "heads" && usedStairs == false)
        {
            //use stairs
            useStairs(stairs);
            usedStairs = true;
        }
    }

    void useStairs(GameObject stairs)
    {
        if (stairs.GetComponent<Stairs>().goesUp == false)
        {
            robberObj.transform.position += Vector3.up * -0.4f;
            robberObj.transform.position += Vector3.right * -0.3f;
        }
        if (stairs.GetComponent<Stairs>().goesUp == true)
        {
            robberObj.transform.position += Vector3.up * 0.4f;
            robberObj.transform.position += Vector3.right * 0.3f;
        }
    }
}
