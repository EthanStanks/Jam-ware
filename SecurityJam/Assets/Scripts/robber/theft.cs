using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class theft : MonoBehaviour
{

    bool walkingRight, walkingLeft; 
    public bool usedStairs = false;
    int temp = 0;
    [SerializeField] private GameObject robberObj;
    //[SerializeField] private Animator robberAnimator;
    [SerializeField] private float robbingSpeed;
    internal Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        walkinAround();
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
    }

    private void OnTriggerEnter2D(Collider2D hitter)
    {
        if (hitter.gameObject.CompareTag("stairs"))
        {
            considerStairs(hitter.gameObject);
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
