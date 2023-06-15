using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.STICKMAN_TAG))
        {
            Coin.coin++;
        }
    }
}
