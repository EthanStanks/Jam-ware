using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceController : MonoBehaviour
{
    [SerializeField] GameObject EntranceObject;
    [SerializeField] bool isWindow;
    [SerializeField] bool isDoor;
    [SerializeField] bool isBroken;
    SpriteRenderer spriteRender;
    [SerializeField] Sprite normalWindowSprite;
    [SerializeField] Sprite brokenWindowSprite;
    [SerializeField] Sprite normalDoorSprite;
    [SerializeField] Sprite brokenDoorSprite;
    [SerializeField] bool spawnRobber;
    [SerializeField] GameObject robberPrefab;
    [SerializeField] public GameObject useGraphic;
    private bool isGaurdOnTopOfEntrance;

    private void Start()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (spawnRobber) SpawnRobber();
        if (isGaurdOnTopOfEntrance && isBroken) NeedsRepair();
        if (isBroken) BrokeEntrance();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBroken && collision.gameObject.tag == "Player")
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = true;
            isGaurdOnTopOfEntrance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBroken && collision.gameObject.tag == "Player")
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = false;
            isGaurdOnTopOfEntrance = false;
        }
    }
    void RepairEntrance()
    {
        if (isWindow)
        {
            spriteRender.sprite = normalWindowSprite;
        }
        else if (isDoor)
        {
            spriteRender.sprite = normalDoorSprite;
        }
        isBroken = false;
        EntranceObject.tag = "Entrance";
        useGraphic.GetComponent<SpriteRenderer>().enabled = false;
    }
    void BrokeEntrance()
    {
        if (isWindow)
        {
            spriteRender.sprite = brokenWindowSprite;
        }
        else if (isDoor)
        {
            spriteRender.sprite = brokenDoorSprite;
        }
        isBroken = true;
        EntranceObject.tag = "Broken";
    }
    void SpawnRobber()
    {
        spawnRobber = false;
        BrokeEntrance();
        GameObject robber = Instantiate(robberPrefab);
        robber.transform.position = new Vector3(robber.transform.position.x, robber.transform.position.y - 1);
    }
    void NeedsRepair()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RepairEntrance();
        }
    }
}
