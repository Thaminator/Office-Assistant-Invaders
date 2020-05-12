using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeDisplay : MonoBehaviour
{

    Player player;
    TextMeshProUGUI lifeText;

    // Start is called before the first frame update
    void Start()
    {
        lifeText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();

    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = player.GetHealth().ToString();
    }
}
