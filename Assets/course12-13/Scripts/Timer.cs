using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private readonly float _startTime = 35f; // начальное время в секундах
    private CoinsManager _coinsManager;
    [SerializeField] private Ball _ball;

    public float CurrentTime { get; private set; }

    private void Awake()
    {
        _ball = FindObjectOfType<Ball>();
        _coinsManager = FindObjectOfType<CoinsManager>();
    }

    void Start()
    {
        CurrentTime = _startTime;
    }

    void Update()
    {
        SetGameTime();
    }

    private void SetGameTime()
    {
        if (CurrentTime > 0 )
        {
            CurrentTime -= Time.deltaTime;
            CurrentTime = Mathf.Max(CurrentTime, 0);
            Debug.Log(CurrentTime.ToString("F0"));
        }
        else if(_coinsManager.СollectedCoins < 6)
        {
            Debug.LogError("вы проиграли");
            _ball.gameObject.SetActive(false);
        }
    }
}
