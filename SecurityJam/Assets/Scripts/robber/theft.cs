using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class theft : MonoBehaviour
{

    bool walkingRight, walkingLeft; 
    public bool usedStairs = false;
    private bool hasGem = false;
    int temp = 0;    //float num = 0.01f;
    [SerializeField] private GameObject robberObj;
    [SerializeField] private SpriteRenderer robberRenderer;
    //[SerializeField] private Animator robberAnimator;
    [SerializeField] private float robbingSpeed;
    internal Collider2D _collider;
    [SerializeField] private GameObject nothing;
    [SerializeField] private GameObject full;
    [SerializeField] public bool isCaught; // this will be set to true if the robber is in the player flashlight
    [SerializeField] float stairsVertical, stairsHorizontal;
    [SerializeField] GameObject myJewel;
    // Start is called before the first frame update
    void Start()
    {
        WalkinAround();
        nothing.SetActive(true);
        full.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (walkingRight)
        {
            WalkRight();
        }
        else if (walkingLeft)
        {
            WalkLeft();
        }
        if (usedStairs)
        {
            temp++;
            if (temp == 50){ usedStairs = false; temp = 0; }
        }
        if(isCaught)
        {
            ThiefDies();
        }
    }

    private void OnTriggerEnter2D(Collider2D hitter)
    {
        if (hitter.gameObject.CompareTag("stairs"))
        {
            ConsiderStairs(hitter.gameObject);
        }
        else if (hitter.gameObject.CompareTag("Valuables"))
        {
            if (hasGem == false)
            {
                hasGem = true;
                nothing.SetActive(false);
                full.SetActive(true);
                myJewel = hitter.gameObject.GetComponent<Valuables>().jewel;
            }
        }
        else 
        {
            walkingLeft = !walkingLeft;
            walkingRight = !walkingRight;
        }
    }

    string CoinFlip()
    {
        int random2 = Random.Range(0, 10);

        if (random2 % 2 == 0)
        {
            return "heads";
        }
        else if (random2 % 2 == 1)
        {
            return "tails";
        }
        else
        {
            return "itsbroke";
        }
    }

    void WalkRight()
    {
        robberObj.transform.position += Vector3.right * robbingSpeed * Time.deltaTime;
    }

    void WalkLeft()
    {
        robberObj.transform.position += Vector3.right * -robbingSpeed * Time.deltaTime;
    }

    void WalkinAround()
    {
        string choice = CoinFlip();
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

    void ConsiderStairs(GameObject stairs)
    {
        string choice = CoinFlip();
        if (choice == "heads" && usedStairs == false)
        {
            //use stairs
            UseStairs(stairs);
            usedStairs = true;
        }
    }

    void UseStairs(GameObject stairs)
    {
        if (stairs.GetComponent<Stairs>().goesUp == false)
        {
            robberObj.transform.position += Vector3.up * -stairsVertical;
            robberObj.transform.position += Vector3.right * -stairsHorizontal;
        }
        if (stairs.GetComponent<Stairs>().goesUp == true)
        {
            robberObj.transform.position += Vector3.up * stairsVertical;
            robberObj.transform.position += Vector3.right * stairsHorizontal;
        }
    }

    void ThiefDies()
    {
        StartCoroutine(Fade());
        if (hasGem)
        {
            //Instantiate(myJewel);
        }
        if (robberRenderer.material.color.a < 0.1f)
        {
            Destroy(robberObj);
        }
    }

    IEnumerator Fade()
    {
        Color c = robberRenderer.material.color;
        while (c.a >= 0.1f)
        {
            Debug.Log("Started Coroutine at timestamp : " + Time.time);
            c.a -= 0.05f;
            robberRenderer.material.color = c;
            yield return new WaitForSeconds(10.1f);
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            //left off here trying to make fade out death for robber man kill him good future me
        }
    }

}
