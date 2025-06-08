using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinsManager _coinsManager;
    private Ball _ball;

    private void Awake()
    {
        _coinsManager = FindObjectOfType<CoinsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _ball = other.GetComponent<Ball>();
        if (_ball != null)
        {
            gameObject.SetActive(false);
            _coinsManager.TakeCoin();
        }
    }
}
