using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    private Coroutine _spawnCoroutine;
    private Vector3 _spawnPoint;
    private Vector3 _bombScale;
    private int _poolSize = 1;

    private Queue<GameObject> _objectPool = new Queue<GameObject>();
    private GameObject _obj;
    [SerializeField] private float _respawnTime;
    [SerializeField] private Vector3 _leftPosition;
    [SerializeField] private Vector3 _rightPosition;
    public GameObject Bomb;

    private void Start()
    {
        SpawnBomb();


        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(Bomb);
          //  obj.transform.position = _spawnPoint;

            _bombScale = new Vector3(0.05f, 0.1f, 0.1f);
            obj.transform.localScale = _bombScale;
            obj.SetActive(false);                        
            _objectPool.Enqueue(obj);                   
        }
    }

    public GameObject GetObject()
    {
        if (_objectPool.Count > 1)
        {
            GameObject obj = _objectPool.Dequeue();  

         //   _bombScale = new Vector3(0.05f, 0.1f, 0.1f);
         //   obj.transform.localScale = _bombScale;
            obj.transform.position = _spawnPoint;

            obj.SetActive(true);                   
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(Bomb);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);  
        _objectPool.Enqueue(obj);  
    }

    private void Update()
    {
        _spawnPoint = new Vector3(Random.Range(_leftPosition.x, _rightPosition.x), _leftPosition.y, 0);
    }

    public void SpawnBomb()
    {
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(SpawnBombCoroutine());
    }

    IEnumerator SpawnBombCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_respawnTime);
            GameObject newObject = GetObject();
        }
    }
}
