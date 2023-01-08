using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap tilemap;
    public ScythText scythText;
    public Vector3Int curPos;
    GameManager gameManager;
    public Vector3Int lastMovement;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = FindObjectOfType<Tilemap>();
        gameManager = FindObjectOfType<GameManager>();
        scythText = FindObjectOfType<ScythText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)) {
            Move(new Vector3Int(-1, 0, 0));
        } else if (Input.GetKeyUp(KeyCode.W)) {
            Move(new Vector3Int(0, 1, 0));
        } else if (Input.GetKeyUp(KeyCode.D))
        {
            Move(new Vector3Int(1, 0, 0));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Move(new Vector3Int(0, -1, 0));
        }
    }

    void Move(Vector3Int dir) {
        Vector3Int newPos = curPos + dir;
        if (tilemap.GetTile(newPos) != null) {
            lastMovement = dir;
            curPos = newPos;
            gameObject.transform.position = tilemap.CellToWorld(newPos);
            if (scythText.scyths > 0)
            {
                if (gameManager.Kill(curPos))
                {
                    scythText.ChangeScyth(-1);
                }


                if (scythText.scyths <= 0)
                {
                    GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("reaper");
                }
            }
            gameManager.Progress();
            
        }
        
    }
}
