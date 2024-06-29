using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 6f;
    [SerializeField]
    private float sprintModifier = 1f;
    [SerializeField]
    private float stamina = 100f;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI nowTreasure;
    [SerializeField]
    private Image staminaImage;
    [SerializeField]
    private List<GameObject> trubels;
    [SerializeField]
    private CanvasGroup map;

    private Rigidbody2D rigBody;
    private bool canDig;
    private Treasure treasureInZone;
    private int treasureCount;
    private List<Treasure> treasureList;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        treasureList = GameObject.Find("Treasures").GetComponentsInChildren<Treasure>().ToList();
        treasureCount = treasureList.Count;
    }


    private void Update()
    {
        if (nowTreasure.text == "" && treasureCount > 0)
            nowTreasure.text = treasureList[0].textHelp;
        if (canDig && Input.GetKeyDown(KeyCode.E) && treasureInZone != null)
        {
            nowTreasure.text = "";
            treasureInZone.SetExcavated();
            treasureList.Remove(treasureInZone);

            treasureInZone.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
            treasureInZone.gameObject.GetComponent<Collider2D>().enabled = false;

            Destroy(treasureInZone);
            gameManager.AddTime(15);
            treasureCount -= 1;
        }
        else if (canDig && Input.GetKeyDown(KeyCode.E) && treasureInZone == null) 
        {
            var rndIndex = Random.Range(0, trubels.Count);
            var truble = Instantiate(trubels[rndIndex]);

            truble.transform.position = transform.position;

            var hole = Instantiate(Resources.Load<GameObject>("Prefabs/Small Hole"));
            hole.transform.position = truble.transform.position;
        }

        if(treasureCount  == 0)
        {
            var winPageCanvas = GameObject.Find("WinPage").GetComponent<CanvasGroup>();

            winPageCanvas.alpha = 1f;
            winPageCanvas.interactable = true;
            winPageCanvas.blocksRaycasts = true;
        }
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.V))
        {
            map.alpha = 1f;
            map.interactable = true;
            map.blocksRaycasts = true;
        }
        else
        {
            map.alpha = 0f;
            map.interactable = false;
            map.blocksRaycasts = false;
        }
        #region
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            sprintModifier = 2;
            stamina -= Time.fixedDeltaTime * 10;
            staminaImage.fillAmount = stamina /100;
        }
        else
        {
            sprintModifier = 1;
            stamina += Time.fixedDeltaTime * 4;
            staminaImage.fillAmount = stamina / 100;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigBody.MovePosition(rigBody.position + new Vector2(0, moveSpeed * Time.fixedDeltaTime * sprintModifier));
            transform.eulerAngles = new Vector3(0,0,0);
        }
        if (Input.GetKey(KeyCode.S))
        { 
            rigBody.MovePosition(rigBody.position + new Vector2(0, -(moveSpeed * Time.fixedDeltaTime * sprintModifier)));
            transform.eulerAngles = new Vector3(0, 0, -180);
        }
        if (Input.GetKey(KeyCode.A))
        { 
            rigBody.MovePosition(rigBody.position + new Vector2(-(moveSpeed * Time.fixedDeltaTime * sprintModifier), 0));
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigBody.MovePosition(rigBody.position + new Vector2(moveSpeed * Time.fixedDeltaTime * sprintModifier, 0));
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        #endregion

    }

    public void SetAbleDig(Treasure treasure)
    {
        canDig = true;
        treasureInZone = treasure;
    }

    public void RemoveAbleDig()
    {
        canDig = false;
        treasureInZone = null;
    }

    public void DamageStamina(float damage)
    {
        stamina -= damage;
        if (stamina < 0) stamina = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Swamp")
        {
            moveSpeed = 0.6f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Swamp")
        {
            moveSpeed = 6f;
        }
    }
}
