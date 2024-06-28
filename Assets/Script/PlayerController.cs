using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.3f;
    [SerializeField]
    private float sprintModifier = 1f;
    [SerializeField]
    private GameManager gameManager;

    private Rigidbody2D rigBody;
    private bool canDig;
    private Treasure treasureInZone;
    private int TreasureCount;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        TreasureCount = GameObject.Find("Treasures").transform.childCount;
    }


    private void Update()
    {
        if (canDig && Input.GetKeyDown(KeyCode.E) && treasureInZone != null)
        {
            treasureInZone.SetExcavated();
            Destroy(treasureInZone);
            gameManager.AddTime(10);
            TreasureCount -= 1;
        }

        if(TreasureCount  == 0)
        {
            var winPageCanvas = GameObject.Find("WinPage").GetComponent<CanvasGroup>();

            winPageCanvas.alpha = 1f;
            winPageCanvas.interactable = true;
            winPageCanvas.blocksRaycasts = true;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigBody.velocity *= 0;
            return;
        }
        #region
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            sprintModifier = 2;
            print("SPRINT");
        }
        else
        {
            sprintModifier = 1;
        }

        if (Input.GetKey(KeyCode.W))
            rigBody.MovePosition(rigBody.position + new Vector2(0, moveSpeed * Time.fixedDeltaTime * sprintModifier));
        if (Input.GetKey(KeyCode.S))
            rigBody.MovePosition(rigBody.position +  new Vector2(0, -(moveSpeed * Time.fixedDeltaTime * sprintModifier)));
        if (Input.GetKey(KeyCode.A))
            rigBody.MovePosition(rigBody.position +  new Vector2(-(moveSpeed * Time.fixedDeltaTime * sprintModifier), 0));
        if (Input.GetKey(KeyCode.D))
            rigBody.MovePosition(rigBody.position +  new Vector2(moveSpeed * Time.fixedDeltaTime * sprintModifier, 0));
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

}
