using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    public int level = 0;
    public void ChangeLevel(int amt) { 
        level += amt;
        GetComponent<TextMeshProUGUI>().text = "Level" + level.ToString();
    }
    
}
