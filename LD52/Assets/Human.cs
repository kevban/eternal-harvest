using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Human : MonoBehaviour
{
    public Vector3Int curPos;
    GameManager gameManager;
    public string type;
    public bool dead = false;
    public Tilemap tilemap;
    public bool shoved = false;
    public int id;
    public GameObject soul;
    public AudioManager audioManager;

    //driver
    public List<Vector3Int> waypoints = new List<Vector3Int>();
    public int curWaypoint = 0;
    public bool forward = true;
    public Vector3Int dir;
    public bool disableMove = false;

    //sniper
    public Vector3Int snipingPos;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        tilemap = FindObjectOfType<Tilemap>();
        switch (type) {
            case "driver":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("driver");
                break;
            case "sniper":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sniper");
                break;
            case "good":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("goodHuman");
                break;
            case "magician":
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("magician");
                break;
            default:
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("human");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dies() {
        if (!dead) {

            dead = true;
            if (type == "sniper")
            {
                gameManager.killPos.Add(snipingPos);
                audioManager.Play("Gunshot");
            }
            else if (type == "good")
            {
                gameManager.SetPoint(100);
                audioManager.Play("GoodHumanHit");
            }
            else if (type == "driver")
            {
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("car");
                audioManager.Play("HumanHit");
            }
            else if (type == "magician")
            {
                gameManager.Shove();
                audioManager.Play("Magic");
            }
            else if (type == "human") {
                audioManager.Play("HumanHit");
            }
            gameManager.SetPoint(-1);
            GameObject.Instantiate(soul, tilemap.CellToWorld(curPos), Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10));
            if (type != "driver") { 
                gameObject.SetActive(false);
            }
        }
        
    }

    public void Shove(Vector3Int direction) {
        curPos += direction;
        Vector3 targetPos = tilemap.CellToWorld(curPos);
        transform.position = targetPos;
        shoved = true;
    }

    // for driver
    public void Move()
    {
        if (!disableMove)
        {
            if (!dead)
            {
                // if car on road, move to next tile. Else move to previous tile
                if (waypoints.Contains(curPos))
                {
                    if (forward)
                    {
                        curWaypoint++;
                    }
                    else
                    {
                        curWaypoint--;
                    }
                    if (curWaypoint >= waypoints.Count - 1 || curWaypoint < 1)
                    {
                        if (waypoints[waypoints.Count - 1] == waypoints[0])
                        {
                            curWaypoint = 0;
                        }
                        else
                        {
                            forward = !forward;
                        }

                    }
                }
                Vector3 targetPos = tilemap.CellToWorld(waypoints[curWaypoint]);
                transform.position = targetPos;
                dir = waypoints[curWaypoint] - curPos;
                gameManager.Kill(waypoints[curWaypoint], type: "driver", ignore: id);
                curPos = waypoints[curWaypoint];
            }
            else
            {
                Vector3Int newPos = curPos + dir;
                if (tilemap.GetTile(newPos) != null)
                {
                    Vector3 targetPos = tilemap.CellToWorld(newPos);
                    transform.position = targetPos;
                    gameManager.Kill(newPos, type: "driver", ignore:id);
                    curPos = newPos;
                }

            }
        }
        else { 
            disableMove = false;
        }
        
        

    }
}
