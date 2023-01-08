using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScythText : MonoBehaviour
{
    public int scyths;

    public void ChangeScyth(int amt) {
        scyths += amt;
        GetComponent<TextMeshProUGUI>().text = "x " + scyths.ToString();
    }
}
