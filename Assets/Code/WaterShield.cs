using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShield : MonoBehaviour
{
    // transform of player, finds player position
    private Transform player;

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<SkySprite>().transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = player.position;
    }
}
