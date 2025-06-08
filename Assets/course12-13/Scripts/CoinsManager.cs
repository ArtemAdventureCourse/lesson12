using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    private int _totalCoins = 7;
    private Timer _timer;
    public int TotalCoins => _totalCoins;
    public int СollectedCoins { get; set; }

    // Start is called before the first frame update

    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
    }
    public void TakeCoin()
    {
        СollectedCoins++;
        if (СollectedCoins >= TotalCoins && _timer.CurrentTime>0)
        {
            Debug.LogWarning("победа ! Все монеты собраны!");
            // здесь можно вызывать победу, сцену и т.п.
        }
    }
}
