using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float _startTime;
    private Vector3 _direction;
    [SerializeField] private Player _player;

    private bool _IsPressedR=>Input.GetKey(KeyCode.R);

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_player.gameObject.activeSelf==false)
            RestartGame(); 
    }

    public void GetDistance(Vector3 npc, Vector3 player)
    {
        float safetyDistance = 6f;
        _direction = npc - player;

        if (_direction.magnitude <= safetyDistance)
            Debug.Log("расстояние до нпс:" + _direction.magnitude.ToString("F0"));    
        else        
            Debug.LogWarning("расстояние до нпс далеко сблизьте расстояние:" + _direction.magnitude.ToString("F0"));
    }
 
    private void GameOver(Player player)
    {
        Debug.Log(player + ":вы проиграли");
        StopTime();
        _direction = Vector3.zero;
        _player.gameObject.SetActive(false);
        Debug.Log("нажмите 'R' для рестарта игры");
    }

    public void StartTime()
    {
        _startTime += Time.deltaTime;
        Debug.Log("Прошло времени: " + _startTime.ToString("F0") + " сек");

        if (_startTime >= 7f)
            GameOver(_player);
    }

    private void RestartGame()
    {  
        if (_IsPressedR)
            _player.gameObject.SetActive(true);  
    }

    public void StopTime()=>  _startTime = 0f;
    
}
