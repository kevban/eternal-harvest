using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scyth : MonoBehaviour
{
    public Vector3Int curPos;
    public ScythText scythText;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        scythText = FindObjectOfType<ScythText>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Human") {
            Human human = collision.gameObject.GetComponent<Human>();
            if (human.type == "driver" && curPos == human.curPos) { 
                Destroy(gameObject);
            }
        }
        else if (collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (curPos == player.curPos && !gameManager.ScythCrushed(curPos))
            {
                scythText.ChangeScyth(1);
                player.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("reaperWithScyth");
                Destroy(gameObject);
            }

        }
    }
}
