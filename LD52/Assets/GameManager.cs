using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Human human;
    public Player player;
    public GameObject reticleDisplay;
    public Scyth scyth;
    public int pointRequired;
    public ScythText scythText;
    public LevelText levelText;
    public Player spawnedPlayer;
    public GameObject nextLevelButton;
    public GameObject restartButton;
    List<Human> humans = new List<Human>();
    public List<GameObject> tutorials = new List<GameObject>();
    public TileBase normalTile;
    public TileBase road1;
    public TileBase road2;
    public TileBase road3;
    public TileBase road4;
    public TileBase road5;
    public TileBase road6;
    public int curTutorial = 0;
    public List<GameObject> reticles = new List<GameObject>();
    List<Scyth> spawnedScyths = new List<Scyth>();
    public List<Vector3Int> killPos = new List<Vector3Int>();
    public AudioManager audioManager;
    public GameObject winGame;
    public GameObject scythPic;
    // Start is called before the first frame update
    void Start()
    {
        levelText.ChangeLevel(1);
        tutorials[0].SetActive(true);
        curTutorial++;
        
    

    }

    public void LoadLevel() {
        scythText.scyths = 0;
        if (spawnedPlayer != null) { 
            Destroy(spawnedPlayer.gameObject);
        }
        foreach (Human human in humans) { 
            Destroy(human.gameObject);
        }
        for (int i = 0; i < 7; i++) {
            for (int j = 0; j < 7; j++) {
                tilemap.SetTile(new Vector3Int(i, j, 0), normalTile);
            }
        }
        foreach (GameObject reticle in reticles) { 
            Destroy(reticle);
        }
        foreach (Scyth scyth in spawnedScyths) {
            if (scyth != null) {
                Destroy(scyth.gameObject);
            }
        
        }
        spawnedScyths.Clear();
        reticles.Clear();
        humans.Clear();
        nextLevelButton.SetActive(false);
        restartButton.SetActive(true);
        List<Vector3Int> driverWaypoints = new List<Vector3Int>();
        switch (levelText.level) {
            case 1:
                scythText.ChangeScyth(1);
                pointRequired = 1;
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(0, 3, 0));
                break;
            case 2:
                scythText.ChangeScyth(2);
                pointRequired = 3;
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(3, 6, 0));
                spawn("human", new Vector3Int(2, 6, 0));
                spawn("human", new Vector3Int(1, 6, 0));
                spawn("scyth", new Vector3Int(6, 6, 0));
                break;
            case 3:
                scythText.ChangeScyth(3);
                pointRequired = 3;
                driverWaypoints.Add(new Vector3Int(6, 6, 0));
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                driverWaypoints.Add(new Vector3Int(6, 4, 0));
                driverWaypoints.Add(new Vector3Int(6, 3, 0));
                driverWaypoints.Add(new Vector3Int(6, 2, 0));
                driverWaypoints.Add(new Vector3Int(6, 1, 0));
                driverWaypoints.Add(new Vector3Int(6, 0, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road1, road1, road1, road1, road1, road1, road1});
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(2, 5, 0));
                spawn("human", new Vector3Int(2, 2, 0));
                spawn("driver", new Vector3Int(6, 6, 0), path: driverWaypoints, dir: new Vector3Int(0, -1, 0));
                break;
            case 4:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(0, 5, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road2, road2, road2, road2, road2 });
                spawn("player", new Vector3Int(0, 4, 0));
                spawn("human", new Vector3Int(0, 5, 0));
                spawn("driver", new Vector3Int(6, 5, 0), path: driverWaypoints, dir: new Vector3Int(-1, 0, 0));
                break;
            case 5:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(3, 6, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 4, 0));
                driverWaypoints.Add(new Vector3Int(3, 3, 0));
                driverWaypoints.Add(new Vector3Int(2, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(0, 3, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road1, road1, road1, road5, road2, road2, road2 });
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(3, 0, 0));
                spawn("driver", new Vector3Int(3, 6, 0), path: driverWaypoints, dir: new Vector3Int(0, -1, 0));
                curTutorial = -1;
                break;
            case 6:
                scythText.ChangeScyth(1);
                pointRequired = 3;
                driverWaypoints.Add(new Vector3Int(6, 2, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(4, 2, 0));
                driverWaypoints.Add(new Vector3Int(3, 2, 0));
                driverWaypoints.Add(new Vector3Int(2, 2, 0));
                driverWaypoints.Add(new Vector3Int(2, 1, 0));
                driverWaypoints.Add(new Vector3Int(2, 0, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road2, road2, road3, road1, road1 });
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(2, 0, 0));
                spawn("human", new Vector3Int(2, 6, 0));
                spawn("driver", new Vector3Int(6, 2, 0), path: driverWaypoints, dir: new Vector3Int(-1, 0, 0));
                break;
            case 7:
                scythText.ChangeScyth(1);
                pointRequired = 3;
                driverWaypoints.Add(new Vector3Int(6, 2, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(4, 2, 0));
                driverWaypoints.Add(new Vector3Int(3, 2, 0));
                driverWaypoints.Add(new Vector3Int(2, 2, 0));
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                driverWaypoints.Add(new Vector3Int(0, 2, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road2, road2, road2, road2, road2 });
                spawn("player", new Vector3Int(4, 3, 0));
                spawn("human", new Vector3Int(2, 0, 0));
                spawn("scyth", new Vector3Int(1, 2, 0));
                spawn("human", new Vector3Int(0, 2, 0));
                spawn("driver", new Vector3Int(6, 2, 0), path: driverWaypoints, dir: new Vector3Int(-1, 0, 0));
                break;
            case 8:
                scythText.ChangeScyth(4);
                pointRequired = 8;
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 4, 0));
                driverWaypoints.Add(new Vector3Int(2, 4, 0));
                driverWaypoints.Add(new Vector3Int(3, 4, 0));
                driverWaypoints.Add(new Vector3Int(4, 4, 0));
                driverWaypoints.Add(new Vector3Int(5, 4, 0));
                driverWaypoints.Add(new Vector3Int(5, 3, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(4, 2, 0));
                driverWaypoints.Add(new Vector3Int(3, 2, 0));
                driverWaypoints.Add(new Vector3Int(2, 2, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[12] { road6, road1, road3, road2, road2, road2, road4, road1, road5, road2, road2, road2  });
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                spawn("driver", new Vector3Int(1, 2, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("driver", new Vector3Int(1, 4, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0), driverStartPos: 2);
                spawn("driver", new Vector3Int(5, 4, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0), driverStartPos: 6);
                spawn("driver", new Vector3Int(5, 2, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0), driverStartPos: 8);
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(1, 6, 0));
                spawn("human", new Vector3Int(6, 4, 0));
                spawn("human", new Vector3Int(5, 0, 0));
                spawn("human", new Vector3Int(0, 2, 0));
                curTutorial = 5;
                break;
            case 9:
                scythText.ChangeScyth(2);
                pointRequired = 1;
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("good", new Vector3Int(3, 3, 0));
                spawn("human", new Vector3Int(6, 6, 0));
                curTutorial = -1;
                break;
            case 10:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 4, 0));
                driverWaypoints.Add(new Vector3Int(3, 3, 0));
                driverWaypoints.Add(new Vector3Int(3, 2, 0));
                driverWaypoints.Add(new Vector3Int(3, 1, 0));
                driverWaypoints.Add(new Vector3Int(3, 0, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[9] { road2, road2, road2, road3, road1, road1, road1, road1, road1 });
                spawn("driver", new Vector3Int(6, 5, 0), path: driverWaypoints, dir: new Vector3Int(-1, 0, 0));
                spawn("player", new Vector3Int(2, 2, 0));
                spawn("human", new Vector3Int(2, 3, 0));
                spawn("scyth", new Vector3Int(6, 3, 0));
                spawn("good", new Vector3Int(3, 0, 0));
                break;
            case 11:
                scythText.ChangeScyth(2);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(0, 1, 0));
                driverWaypoints.Add(new Vector3Int(0, 2, 0));
                driverWaypoints.Add(new Vector3Int(0, 3, 0));
                driverWaypoints.Add(new Vector3Int(0, 4, 0));
                driverWaypoints.Add(new Vector3Int(0, 5, 0));
                driverWaypoints.Add(new Vector3Int(0, 6, 0));
                driverWaypoints.Add(new Vector3Int(1, 6, 0));
                driverWaypoints.Add(new Vector3Int(2, 6, 0));
                driverWaypoints.Add(new Vector3Int(3, 6, 0));
                driverWaypoints.Add(new Vector3Int(4, 6, 0));
                driverWaypoints.Add(new Vector3Int(5, 6, 0));
                driverWaypoints.Add(new Vector3Int(6, 6, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[12] { road1, road1, road1, road1, road1, road3, road2, road2, road2, road2, road2, road4 });
                spawn("driver", new Vector3Int(0, 1, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                List<Vector3Int> driverWaypoints2 = new List<Vector3Int>();
                driverWaypoints2.Add(new Vector3Int(1, 0, 0));
                driverWaypoints2.Add(new Vector3Int(2, 0, 0));
                driverWaypoints2.Add(new Vector3Int(3, 0, 0));
                driverWaypoints2.Add(new Vector3Int(4, 0, 0));
                driverWaypoints2.Add(new Vector3Int(5, 0, 0));
                driverWaypoints2.Add(new Vector3Int(6, 0, 0));
                driverWaypoints2.Add(new Vector3Int(6, 1, 0));
                driverWaypoints2.Add(new Vector3Int(6, 2, 0));
                driverWaypoints2.Add(new Vector3Int(6, 3, 0));
                driverWaypoints2.Add(new Vector3Int(6, 4, 0));
                driverWaypoints2.Add(new Vector3Int(6, 5, 0));
                driverWaypoints2.Add(new Vector3Int(6, 6, 0));
                tilemap.SetTiles(driverWaypoints2.ToArray(), new TileBase[12] { road2, road2, road2, road2, road2, road5, road1, road1, road1, road1, road1, road4 });
                spawn("driver", new Vector3Int(1, 0, 0), path: driverWaypoints2, dir: new Vector3Int(0, 1, 0));
                spawn("good", new Vector3Int(6, 6, 0));
                spawn("player", new Vector3Int(1, 1, 0));
                break;
            case 12:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(0, 1, 0));
                driverWaypoints.Add(new Vector3Int(0, 2, 0));
                driverWaypoints.Add(new Vector3Int(0, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(2, 3, 0));
                driverWaypoints.Add(new Vector3Int(3, 3, 0));
                driverWaypoints.Add(new Vector3Int(4, 3, 0));
                driverWaypoints.Add(new Vector3Int(5, 3, 0));
                driverWaypoints.Add(new Vector3Int(6, 3, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[10] { road1, road1, road3, road2, road2, road2, road2, road2, road2, road2 });
                spawn("driver", new Vector3Int(0, 1, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("scyth", new Vector3Int(4, 3, 0));
                spawn("player", new Vector3Int(1, 4, 0));
                spawn("human", new Vector3Int(6, 6, 0));
                spawn("scyth", new Vector3Int(5, 6, 0));
                spawn("good", new Vector3Int(6, 5, 0));
                spawn("good", new Vector3Int(5, 5, 0));
                spawn("good", new Vector3Int(4, 6, 0));
                curTutorial = 6;
                break;
            case 13:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                spawn("human", new Vector3Int(0, 0, 0));
                spawn("player", new Vector3Int(0, 1, 0));
                spawn("sniper", new Vector3Int(6, 6, 0), snipingPos: new Vector3Int(0, 0, 0));
                curTutorial = -1;
                break;
            case 14:
                scythText.ChangeScyth(1);
                pointRequired = 2;
                driverWaypoints.Add(new Vector3Int(6, 2, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(4, 2, 0));
                driverWaypoints.Add(new Vector3Int(4, 3, 0));
                driverWaypoints.Add(new Vector3Int(4, 4, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 6, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road6, road1, road1, road1, road1 });
                spawn("driver", new Vector3Int(6, 2, 0), path: driverWaypoints, dir: new Vector3Int(-1, 0, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("sniper", new Vector3Int(0, 1, 0), snipingPos: new Vector3Int(4, 2, 0));
                break;
            case 15:
                scythText.ChangeScyth(1);
                pointRequired = 3;
                driverWaypoints.Add(new Vector3Int(0, 1, 0));
                driverWaypoints.Add(new Vector3Int(0, 2, 0));
                driverWaypoints.Add(new Vector3Int(0, 3, 0));
                driverWaypoints.Add(new Vector3Int(0, 4, 0));
                driverWaypoints.Add(new Vector3Int(0, 5, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[11] { road1, road1, road1, road1, road3, road2, road2, road2, road2, road2, road2 });
                spawn("driver", new Vector3Int(0, 1, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("player", new Vector3Int(6, 6, 0));
                spawn("sniper", new Vector3Int(6, 3, 0), snipingPos: new Vector3Int(0, 5, 0));
                spawn("good", new Vector3Int(1, 5, 0));
                spawn("human", new Vector3Int(0, 6, 0));
                curTutorial = 7;
                break;
            case 16:
                scythText.ChangeScyth(2);
                pointRequired = 3;
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("sniper", new Vector3Int(6, 3, 0), snipingPos: new Vector3Int(0, 5, 0));
                spawn("magician", new Vector3Int(3, 3, 0));
                spawn("human", new Vector3Int(0, 6, 0));
                curTutorial = -1;
                break;
            case 17:
                scythText.ChangeScyth(2);
                pointRequired = 3;
                driverWaypoints.Add(new Vector3Int(2, 0, 0));
                driverWaypoints.Add(new Vector3Int(2, 1, 0));
                driverWaypoints.Add(new Vector3Int(2, 2, 0));
                driverWaypoints.Add(new Vector3Int(2, 3, 0));
                driverWaypoints.Add(new Vector3Int(2, 4, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 6, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road1, road1, road1, road1, road1, road1, road1 });
                spawn("driver", new Vector3Int(2, 0, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("magician", new Vector3Int(4, 2, 0));
                spawn("player", new Vector3Int(4, 4, 0));
                spawn("good", new Vector3Int(2, 6, 0));
                spawn("sniper", new Vector3Int(6, 3, 0), snipingPos: new Vector3Int(2, 6, 0));
                break;
            case 18:
                scythText.ChangeScyth(2);
                pointRequired = 4;
                driverWaypoints.Add(new Vector3Int(0, 5, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road2, road2, road2, road2, road2 });
                spawn("driver", new Vector3Int(0, 5, 0), path: driverWaypoints, dir: new Vector3Int(1, 0, 0));
                spawn("magician", new Vector3Int(1, 1, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(6, 6, 0));
                spawn("sniper", new Vector3Int(0, 1, 0), snipingPos: new Vector3Int(2, 5, 0));
                break;
            case 19:
                scythText.ChangeScyth(4);
                pointRequired = 7;
                driverWaypoints.Add(new Vector3Int(3, 0, 0));
                driverWaypoints.Add(new Vector3Int(3, 1, 0));
                driverWaypoints.Add(new Vector3Int(3, 2, 0));
                driverWaypoints.Add(new Vector3Int(3, 3, 0));
                driverWaypoints.Add(new Vector3Int(3, 4, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 6, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road1, road1, road1, road1, road1, road1, road1 });
                spawn("driver", new Vector3Int(3, 0, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("magician", new Vector3Int(2, 0, 0));
                spawn("magician", new Vector3Int(1, 0, 0));
                spawn("magician", new Vector3Int(0, 0, 0));
                spawn("player", new Vector3Int(1, 1, 0));
                spawn("human", new Vector3Int(2, 4, 0));
                spawn("human", new Vector3Int(1, 5, 0));
                spawn("human", new Vector3Int(0, 6, 0));
                break;
            case 20:
                scythText.ChangeScyth(2);
                pointRequired = 5;
                driverWaypoints.Add(new Vector3Int(0, 5, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(6, 5, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road2, road2, road2, road2, road2, road2, road2 });
                spawn("driver", new Vector3Int(0, 5, 0), path: driverWaypoints, dir: new Vector3Int(1, 0, 0));
                spawn("magician", new Vector3Int(1, 1, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(1, 4, 0));
                spawn("human", new Vector3Int(3, 6, 0));
                spawn("human", new Vector3Int(5, 4, 0));
                break;
            case 21:
                scythText.ChangeScyth(2);
                pointRequired = 2;
                spawn("magician", new Vector3Int(2, 2, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(3, 5, 0));
                spawn("good", new Vector3Int(3, 6, 0));
                spawn("good", new Vector3Int(4, 5, 0));
                spawn("good", new Vector3Int(2, 5, 0));
                spawn("good", new Vector3Int(3, 4, 0));
                break;
            case 22:
                scythText.ChangeScyth(3);
                pointRequired = 4;
                driverWaypoints.Add(new Vector3Int(1, 0, 0));
                driverWaypoints.Add(new Vector3Int(1, 1, 0));
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 4, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(1, 6, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[7] { road1, road1, road1, road1, road1, road1, road1 });
                spawn("driver", new Vector3Int(1, 0, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("good", new Vector3Int(4, 6, 0));
                spawn("good", new Vector3Int(4, 5, 0));
                spawn("good", new Vector3Int(4, 4, 0));
                spawn("good", new Vector3Int(4, 3, 0));
                spawn("good", new Vector3Int(4, 2, 0));
                spawn("good", new Vector3Int(4, 1, 0));
                spawn("good", new Vector3Int(4, 0, 0));
                spawn("scyth", new Vector3Int(5, 3, 0));
                spawn("human", new Vector3Int(6, 6, 0));
                spawn("human", new Vector3Int(1, 3, 0));
                spawn("magician", new Vector3Int(2, 6, 0));
                spawn("player", new Vector3Int(3, 5, 0));
                break;
            case 23:
                scythText.ChangeScyth(2);
                pointRequired = (5);
                driverWaypoints.Add(new Vector3Int(1, 1, 0));
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 4, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 4, 0));
                driverWaypoints.Add(new Vector3Int(5, 3, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(5, 1, 0));
                driverWaypoints.Add(new Vector3Int(4, 1, 0));
                driverWaypoints.Add(new Vector3Int(3, 1, 0));
                driverWaypoints.Add(new Vector3Int(2, 1, 0));
                driverWaypoints.Add(new Vector3Int(1, 1, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[16] { road6, road1, road1, road1, road3, road2, road2, road2, road4, road1, road1, road1, road5, road2, road2, road2 });
                spawn("driver", new Vector3Int(1, 1, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("human", new Vector3Int(6, 5, 0));
                spawn("human", new Vector3Int(3, 6, 0));
                spawn("magician", new Vector3Int(4, 4, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("magician", new Vector3Int(2, 4, 0));
                break;
            case 24:
                scythText.ChangeScyth(3);
                pointRequired = (7);
                driverWaypoints.Add(new Vector3Int(1, 1, 0));
                driverWaypoints.Add(new Vector3Int(1, 2, 0));
                driverWaypoints.Add(new Vector3Int(1, 3, 0));
                driverWaypoints.Add(new Vector3Int(1, 4, 0));
                driverWaypoints.Add(new Vector3Int(1, 5, 0));
                driverWaypoints.Add(new Vector3Int(2, 5, 0));
                driverWaypoints.Add(new Vector3Int(3, 5, 0));
                driverWaypoints.Add(new Vector3Int(4, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 5, 0));
                driverWaypoints.Add(new Vector3Int(5, 4, 0));
                driverWaypoints.Add(new Vector3Int(5, 3, 0));
                driverWaypoints.Add(new Vector3Int(5, 2, 0));
                driverWaypoints.Add(new Vector3Int(5, 1, 0));
                driverWaypoints.Add(new Vector3Int(4, 1, 0));
                driverWaypoints.Add(new Vector3Int(3, 1, 0));
                driverWaypoints.Add(new Vector3Int(2, 1, 0));
                driverWaypoints.Add(new Vector3Int(1, 1, 0));
                tilemap.SetTiles(driverWaypoints.ToArray(), new TileBase[16] { road6, road1, road1, road1, road3, road2, road2, road2, road4, road1, road1, road1, road5, road2, road2, road2 });
                spawn("driver", new Vector3Int(1, 1, 0), path: driverWaypoints, dir: new Vector3Int(0, 1, 0));
                spawn("human", new Vector3Int(6, 5, 0));
                spawn("human", new Vector3Int(2, 6, 0));
                spawn("human", new Vector3Int(2, 3, 0));
                spawn("magician", new Vector3Int(6, 3, 0));
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("magician", new Vector3Int(1, 6, 0));
                spawn("sniper", new Vector3Int(6, 2, 0), snipingPos: new Vector3Int(3, 1));
                break;
            case -1:
                pointRequired = 3;
                spawn("player", new Vector3Int(0, 0, 0));
                spawn("human", new Vector3Int(3, 3, 0));
                spawn("sniper", new Vector3Int(6, 6, 0), snipingPos: new Vector3Int(0, 3, 0));
                spawn("scyth", new Vector3Int(4, 4, 0));
                break;
            default:
                WinGame();
                break;
        }
    }

    public void WinGame() { 
        winGame.SetActive(true);
        restartButton.SetActive(false);
        scythPic.SetActive(false);
        scythText.gameObject.SetActive(false);
        levelText.gameObject.SetActive(false);
    }

    public void CloseTutorial() {
        if (curTutorial == 1) {
            audioManager.Play("Background");
            scythText.gameObject.SetActive(true);
            levelText.gameObject.SetActive(true);
            scythPic.SetActive(true);
            restartButton.SetActive(true);
        }
        foreach (GameObject tutorial in tutorials) { 
            tutorial.SetActive(false);
        }
        LoadLevel();
    }



    public bool ScythCrushed(Vector3Int pos) {
        foreach (Human human in humans) {
            if (human.type == "driver" && human.curPos == pos) { 
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    public void NextLevel() {
        levelText.ChangeLevel(1);
        if (curTutorial == -1)
        {
            LoadLevel();
        }
        else { 
            tutorials[curTutorial].SetActive(true);
            curTutorial++;
        }
    }

    public void SetPoint(int point) {
        pointRequired += point;
        if (pointRequired <= 0)
        {
            nextLevelButton.SetActive(true);
            restartButton.SetActive(false);
        }
    }

    public void Progress() {
        foreach (Vector3Int pos in killPos) {
            Kill(pos);
        }
        foreach (Human human in humans) {
            if (human.type == "driver") {
                human.Move();
                if (human.curPos == spawnedPlayer.curPos && scythText.scyths > 0 && !human.dead) {
                    Kill(human.curPos);
                    scythText.ChangeScyth(-1);
                }
            }
            
        }
        foreach (Vector3Int pos in killPos)
        {
            Kill(pos);
        }
        killPos.Clear();
    }

    public bool Kill(Vector3Int killPos, int? ignore = null, string? debug = null, string? type = null) {
        print(debug);
        bool killed = false;
        foreach (Human human in humans) {
            if (human.curPos == killPos && !human.dead && human.id != ignore) {
                if (type == "driver") {
                    audioManager.Play("CarHit");
                }
                human.Dies();
                killed = true;
          }
        }
        return killed;
    }

    public void Shove() {
        
        int shoveCount = 0;
        int runCount = 0;
        List<Vector3Int> runOverPos = new List<Vector3Int>();
        List<int> driverIds = new List<int>();
        while (shoveCount < humans.Count) {
            runCount++;
            if (runCount > 100) {
                print("force broke");
                break;
            }
            foreach (Human human in humans)
            {
                Vector3Int targetPos = human.curPos + spawnedPlayer.lastMovement;
                if (!human.shoved)
                {
                    if (tilemap.GetTile(targetPos) != null)
                    {
                        if (human.type == "driver")
                        {
                            human.Shove(spawnedPlayer.lastMovement);
                            human.disableMove = true;
                            shoveCount++;
                            runOverPos.Add(human.curPos);
                            driverIds.Add(human.id);
                        }
                        else {
                            bool inTheWay = false;
                            foreach (Human human2 in humans)
                            {
                                if (human2.curPos == targetPos)
                                {
                                    inTheWay = true;
                                    if (human2.shoved)
                                    {
                                        human.shoved = true;
                                        shoveCount++;
                                    }
                                }
                            }
                            if (!inTheWay)
                            {
                                human.Shove(spawnedPlayer.lastMovement);
                                shoveCount++;
                            }
                        }
                     
                    }
                    else
                    {
                        human.shoved = true;
                        shoveCount++;
                    }
                }

            }
        }
        for (int i = 0; i < runOverPos.Count; i++) {
            Kill(runOverPos[i], ignore: driverIds[i], debug: "shove");
        }
        foreach (Human human in humans) { 
            human.shoved = false;
        }
        
    }


    public void spawn(string type, Vector3Int position, List<Vector3Int>? path = null, Vector3Int? dir = null, Vector3Int? snipingPos = null, int? driverStartPos = null) {
        Vector3 worldPos = tilemap.CellToWorld(position);
        
        switch (type)
        {
            case "player":
                spawnedPlayer = GameObject.Instantiate(player, worldPos, Quaternion.identity);
                spawnedPlayer.curPos = position;
                break;
            case "human":
                Human spawnedHuman = GameObject.Instantiate(human, worldPos, Quaternion.identity);
                spawnedHuman.curPos = position;
                spawnedHuman.type = type;
                spawnedHuman.id = humans.Count;
                humans.Add(spawnedHuman);
                break;
            case "good":
                Human spawnedGood = GameObject.Instantiate(human, worldPos, Quaternion.identity);
                spawnedGood.curPos = position;
                spawnedGood.type = type;
                spawnedGood.id = humans.Count;
                humans.Add(spawnedGood);
                break;
            case "driver":
                Human spawnedDriver = GameObject.Instantiate(human, worldPos, Quaternion.identity);
                spawnedDriver.curPos = position;
                spawnedDriver.waypoints = path;
                spawnedDriver.dir = (Vector3Int)dir;
                spawnedDriver.type = type;
                spawnedDriver.id = humans.Count;
                humans.Add(spawnedDriver);
                if (driverStartPos != null) {
                    spawnedDriver.curWaypoint = (int)driverStartPos;
                }
                break;
            case "sniper":
                Vector3 snipingWorldPos = tilemap.CellToWorld((Vector3Int)snipingPos);
                Human spawnedSniper = GameObject.Instantiate(human, worldPos, Quaternion.identity);
                GameObject spawnedReticle = GameObject.Instantiate(reticleDisplay, snipingWorldPos, Quaternion.identity);
                spawnedSniper.curPos = position;
                spawnedSniper.snipingPos = (Vector3Int)snipingPos;
                spawnedSniper.type = type;
                spawnedSniper.id = humans.Count;
                humans.Add(spawnedSniper);
                reticles.Add(spawnedReticle);
                break;
            case "scyth":
                Scyth spawnedScyth = GameObject.Instantiate(scyth, worldPos, Quaternion.identity);
                spawnedScyth.curPos = position;
                spawnedScyths.Add(spawnedScyth);
                break;
            case "magician":
                Human spawnedMagician = GameObject.Instantiate(human, worldPos, Quaternion.identity);
                spawnedMagician.curPos = position;
                spawnedMagician.type = type;
                spawnedMagician.id = humans.Count;
                humans.Add(spawnedMagician);
                break;
            default:
                break;
        }
        
    }

}

