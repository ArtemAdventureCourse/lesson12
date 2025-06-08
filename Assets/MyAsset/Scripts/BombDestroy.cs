using UnityEngine;

public class BombDestroy : MonoBehaviour
{
    private BombPool _objectPool;

    private void Awake()
    {
       SetBombPool();
    }

    private void SetBombPool() => _objectPool = FindObjectOfType<BombPool>();

    private void OnCollisionEnter(Collision collision)
    {
        if (IsBomb(collision))
        {
            Debug.Log("Объект вошел в триггер " + collision.gameObject.name);
            ReturnBombInPool(collision);
        }
    }

    public bool IsBomb(Collision collision) => collision.gameObject.CompareTag("DestroyBomb");

    private void ReturnBombInPool(Collision bomb) => _objectPool.ReturnObject(bomb.gameObject);

}