using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] SpriteRenderer cameraSpriteRenderer;
    [SerializeField] float rotateCooldown;
    [SerializeField] GameObject alertIntruder;
    [SerializeField] GameObject alertBreakIn;
    private float rotateTime;
    [SerializeField] bool seenBreakIn;
    [SerializeField] bool seenIntruder;
    [SerializeField] GameObject redLight;
    [SerializeField] GameObject raycastObj;
    [SerializeField] float raycastDistance;
    [SerializeField] float cameraDirection;
    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        cameraDirection = 0f;
    }
    private void Update()
    {
        FlipCamera();
        SeenBreakIn(seenBreakIn);
        SeenIntruder(seenIntruder);
        ScanArea();
    }

    private void FlipCamera()
    {
        if (Time.time > rotateTime)
        {
            rotateTime = Time.time + rotateCooldown;
            seenBreakIn = false; // resets break in alert
            seenIntruder = false; // resets intruder alert
            if (cameraSpriteRenderer.flipX == false) // if camera is facing to the left
            {
                RightFacingCamera(); // makes camera face the right
            }
            else // if camera is facing to the right
            {
                LeftFacingCamera(); // makes camera face the left
            }
        }
    }
    void RightFacingCamera()
    {
        cameraSpriteRenderer.flipX = true; // make it look to the right
        alertIntruder.transform.localPosition = new Vector3(-0.312000006f, 0.0240000002f, 0.248394936f); // move the intruder alert behind the camera
        alertBreakIn.transform.localPosition = new Vector3(-0.391000003f, 0.0500000007f, 0.248394936f); // move the break in alert behind the camera
        redLight.transform.localPosition = new Vector3(-0.064000003f, 0.0200001374f, 0.248394936f); // move the break in alert behind the camera
        raycastObj.transform.localPosition = new Vector3(0.222000003f, -0.9f, 0); // move the raycast obj
        cameraDirection = 1f;
    }
    void LeftFacingCamera()
    {
        cameraSpriteRenderer.flipX = false; // make it look to the left
        alertIntruder.transform.localPosition = new Vector3(0.286000013f, -0.00899999961f, 0.248394936f); // move the intruder alert behind the camera
        alertBreakIn.transform.localPosition = new Vector3(0.389999986f, 0.0399999991f, 0.248394936f); // move the break in alert behind the camera
        redLight.transform.localPosition = new Vector3(0.0659998208f, 0.0200001374f, 0.248394936f); // move the red light to the correct position
        raycastObj.transform.localPosition = new Vector3(-0.206f, -0.9f, 0); // move the raycast obj
        cameraDirection = -1f;
    }
    void SeenBreakIn(bool isSeen)
    {
        alertBreakIn.SetActive(isSeen);
    }
    void SeenIntruder(bool isSeen)
    {
        if (isSeen == true) SeenBreakIn(false);
        alertIntruder.SetActive(isSeen);
    }

    void ScanArea()
    {
        Vector3 origin = raycastObj.transform.position;
        Vector3 direction = transform.right * cameraDirection;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, raycastDistance, layerMask);

        foreach (var hit in hits)
        {
            if (hit.transform != null) // if it hits something
            {
                if (hit.transform.gameObject.CompareTag("Robber")) // if that something is a robber
                {
                    seenIntruder = true;
                    //Debug.DrawRay(origin, direction, Color.red); // shows the raytrace line if it hits a robber
                }
                else if (hit.transform.gameObject.CompareTag("Broken")) // if that something is in broken state
                {
                    seenBreakIn = true;
                    //Debug.DrawRay(origin, direction, Color.blue); // shows the raytrace line if it hits a broken obj
                }
            }
        }
    }
}
