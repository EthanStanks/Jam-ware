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
    private bool isGuardOnTopOfEntrance;

    private void Start()
    {
        spriteRender = GetComponentInChildren<SpriteRenderer>();
        GameManager.instance.lstEntrances.Add(EntranceObject);
    }
    private void Update()
    {
        if (spawnRobber) SpawnRobber();
        if (isGuardOnTopOfEntrance && isBroken) NeedsRepair();
        if (isBroken) BrokeEntrance();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBroken && collision.gameObject.tag == "Player")
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = true;
            isGuardOnTopOfEntrance = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBroken && collision.gameObject.tag == "Player")
        {
            useGraphic.GetComponent<SpriteRenderer>().enabled = false;
            isGuardOnTopOfEntrance = false;
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
    public void SpawnRobber()
    {
        spawnRobber = false;
        BrokeEntrance();
        GameObject Robber = Instantiate(GameManager.instance.RobberPrefab, transform.position, transform.rotation);
        Robber.name = "Robber";
        Robber.GetComponent<theft>().isCaught = false;
        GameManager.instance.lstRobbers.Add(Robber);
    }
    void NeedsRepair()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RepairEntrance();
        }
    }
}
