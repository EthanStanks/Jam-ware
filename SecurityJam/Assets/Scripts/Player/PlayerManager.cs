using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObj;
    [SerializeField] private float guardSpeed;
    [SerializeField] private SpriteRenderer guardRenderer;
    [SerializeField] private SpriteRenderer flashlightRenderer;
    [SerializeField] private Animator guardAnimator;
    [SerializeField] private Transform flashlightTransform;
    [SerializeField] private GameObject lightRight;
    [SerializeField] private GameObject lightLeft;
    [SerializeField] private GameObject interactable;


    void Update()
    {
        MovePlayer();
        FlipPlayerSprites();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FlipPlayerSprites()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            guardRenderer.flipX = false;
            flashlightRenderer.flipX = false;
            flashlightTransform.localPosition = new Vector3(0.15f, flashlightTransform.localPosition.y, flashlightTransform.localPosition.z);
            lightRight.SetActive(true);
            lightLeft.SetActive(false);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            guardRenderer.flipX = true;
            flashlightRenderer.flipX = true;
            flashlightTransform.localPosition = new Vector3(-0.2f, flashlightTransform.localPosition.y, flashlightTransform.localPosition.z);
            lightRight.SetActive(false);
            lightLeft.SetActive(true);

        }
    }

    void MovePlayer()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            playerGameObj.transform.position += Vector3.right * guardSpeed * Time.deltaTime;
            guardAnimator.SetBool("isWalking", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerGameObj.transform.position += Vector3.right * -guardSpeed * Time.deltaTime;
            guardAnimator.SetBool("isWalking", true);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            guardAnimator.SetBool("isWalking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("stairs") || collision.gameObject.CompareTag("Valuables") || collision.gameObject.CompareTag("Broken"))
        {
            interactable = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            interactable = null;
    }

    void Interact()//context sensitive interaction
    {
        if (interactable != null)
        {
            if (interactable.CompareTag("stairs"))
            {
                useStairs(interactable);
                //interactable = null;
            }
        }
    }

    void useStairs(GameObject stairs)
    {
        if (stairs.GetComponent<Stairs>().goesUp == false)
        {
            playerGameObj.transform.position += Vector3.up * -0.4f;
            playerGameObj.transform.position += Vector3.right * -0.3f;
        }
        if (stairs.GetComponent<Stairs>().goesUp == true)
        {
            playerGameObj.transform.position += Vector3.up * 0.4f;
            playerGameObj.transform.position += Vector3.right * 0.3f;
        }
    }

}
