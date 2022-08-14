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
    [SerializeField] private Transform flashlightGameObj;


    void Update()
    {
        MovePlayer();
        FlipPlayerSprites();

    }

    void FlipPlayerSprites()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            guardRenderer.flipX = false;
            flashlightRenderer.flipX = false;
            flashlightGameObj.localPosition = new Vector3(0.15f, flashlightGameObj.localPosition.y, flashlightGameObj.localPosition.z);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            guardRenderer.flipX = true;
            flashlightRenderer.flipX = true;
            flashlightGameObj.localPosition = new Vector3(-0.2f, flashlightGameObj.localPosition.y, flashlightGameObj.localPosition.z);

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
}
