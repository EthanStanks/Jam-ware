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
    [SerializeField] float stairsVertical, stairsHorizontal;
    [SerializeField] GameObject raycastObj;
    [SerializeField] float raycastDistance;
    float guardDirection;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject heldObject;
    Vector3 Shift = new Vector3(0, 2, 0);

    private void Start()
    {
        guardDirection = 0f;
    }
    void Update()
    {
        MovePlayer();
        FlipPlayerSprites();
        FlashlightRaycast();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (heldObject != null)
        {
            heldObject.transform.position = playerGameObj.transform.position;
            heldObject.transform.position += Shift;
        }
    }

    void FlipPlayerSprites()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) GuardFacingRight();
        else if (Input.GetAxisRaw("Horizontal") < 0) GuardFacingLeft();
    }
    void GuardFacingRight()
    {
        guardRenderer.flipX = false;
        flashlightRenderer.flipX = false;
        flashlightTransform.localPosition = new Vector3(0.15f, flashlightTransform.localPosition.y, flashlightTransform.localPosition.z);
        raycastObj.transform.localPosition = new Vector3(0.639999986f, -0.0799999982f, 0);
        lightRight.SetActive(true);
        lightLeft.SetActive(false);
        guardDirection = 1f;
    }
    void GuardFacingLeft()
    {
        guardRenderer.flipX = true;
        flashlightRenderer.flipX = true;
        flashlightTransform.localPosition = new Vector3(-0.2f, flashlightTransform.localPosition.y, flashlightTransform.localPosition.z);
        raycastObj.transform.localPosition = new Vector3(-0.74000001f, -0.0799999982f, 0);
        lightRight.SetActive(false);
        lightLeft.SetActive(true);
        guardDirection = -1f;
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
        if (collision.gameObject.CompareTag("stairs") || collision.gameObject.CompareTag("Valuables") || collision.gameObject.CompareTag("Broken") || collision.gameObject.CompareTag("Storage"))
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
            else if (interactable.CompareTag("Valuables"))
            {
                heldObject = interactable;
            }
            else if (interactable.CompareTag("Storage"))
            {
                if (interactable.GetComponent<Storage>().broken && heldObject.GetComponent<Valuables>().jewelType == interactable.GetComponent<Storage>().jewelType)
                {
                    useStorage(interactable);
                }
            }
        }
    }

    void useStairs(GameObject stairs)
    {
        if (stairs.GetComponent<Stairs>().goesUp == false)
        {
            playerGameObj.transform.position += Vector3.up * -stairsVertical;
            playerGameObj.transform.position += Vector3.right * -stairsHorizontal;
        }
        if (stairs.GetComponent<Stairs>().goesUp == true)
        {
            playerGameObj.transform.position += Vector3.up * stairsVertical;
            playerGameObj.transform.position += Vector3.right * stairsHorizontal;
        }
    }

    void useStorage(GameObject storage)
    {
        storage.GetComponent<Storage>().Renderer.sprite = storage.GetComponent<Storage>().closed;
        storage.GetComponent<Storage>().broken = false;
        Destroy(heldObject);
    }

    void FlashlightRaycast()
    {
        Vector3 origin = raycastObj.transform.position;
        Vector3 direction = transform.right * guardDirection;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, raycastDistance, layerMask);
        foreach (var hit in hits)
        {
            if (hit.transform != null) // if it hits something
            {
                if (hit.transform.gameObject.CompareTag("Robber")) // if that something is a robber
                {
                    if(hit.transform.gameObject.GetComponent<theft>() != null)
                    {
                        hit.transform.gameObject.GetComponent<theft>().isCaught = true;
                    }
                }
            }
        }
    }
}
