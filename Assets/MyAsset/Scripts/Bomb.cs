using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;

    private bool IsPlayer(Collision player) => player.gameObject.CompareTag("Player");

    private bool PressYKey => Input.GetKeyDown(KeyCode.Y);

    private void Awake()
    {
        SetTrail();
    }

    private void SetTrail()
    {
        _trail = GetComponentInChildren<TrailRenderer>();
        _trail.enabled = false;
    }

    private void Update()
    {
        ViewTrail();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsPlayer(collision))
        {
            Debug.LogError("вы умерли " + collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }

    private void ViewTrail()
    {
        if (PressYKey)
            _trail.enabled = true;

    }
}
