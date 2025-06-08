using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Npc _npc;
    [SerializeField] private Material _floorMaterial;
    [SerializeField] private Transform _npcTransform;
    [SerializeField] private float _distanceForNpc;
    [SerializeField] private GameObject _floor;
    [SerializeField] private float _colorSpeed = 4f;
    [SerializeField] private float _speed;

    private Coroutine _coroutine;
    private Coroutine _moveCoroutine;
    private GameManager _gameManager;

    public Queue<Vector3> PointMove { get; set; }
    public bool IsSafetyZone => _distanceForNpc <= 6;

    private bool PressedKeyUp => Input.GetKey(KeyCode.UpArrow);
    private bool PressedKeyDown => Input.GetKey(KeyCode.DownArrow);
    private bool PressedKeyLeft => Input.GetKey(KeyCode.LeftArrow);
    private bool PressedKeyRight => Input.GetKey(KeyCode.RightArrow);

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _npc = FindObjectOfType<Npc>();
    }

    private void Update()
    {
        DistanceForNPC();
        ChangeColorDepengingDistance();
        Move();
      _gameManager.GetDistance(_npc.transform.position, transform.position);

        if (IsSafetyZone )
        {
            LookAtForTarget(_npc.transform.position);          
            _gameManager.StopTime();
        }
        else 
        {
            _gameManager.StartTime();

            if (gameObject.activeSelf)
            {
                LookAtForTarget(transform.position);
            }
        }
    }

    private void Move()
    {
        if (PressedKeyUp)
            HandleMove(Vector3.forward);
        else if (PressedKeyDown)
            HandleMove(Vector3.back);
        else if (PressedKeyLeft)
            HandleMove(Vector3.left);
        else if (PressedKeyRight)
            HandleMove(Vector3.right);
    }

    private void DistanceForNPC()
    {
        Vector3 direction = _npcTransform.position - transform.position;
        _distanceForNpc = direction.magnitude;
    }

    private void ChangeColorDepengingDistance()
    {
        if (IsSafetyZone == false)
            _floorMaterial.color = Color.Lerp(Color.red, Color.white, _colorSpeed * Time.deltaTime);
        else
            _floorMaterial.color = Color.Lerp(Color.white, Color.red, _colorSpeed * Time.deltaTime);
    }

    private void LookAtForTarget(Vector3 targetDirection)
    {
        if (!gameObject.activeInHierarchy)
        {
            Debug.LogWarning("Невозможно запустить корутину: объект Игрок неактивен.");
            return;
        }

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(AsyncLookAtForTarget(targetDirection));
    }

    private void HandleMove(Vector3 direction)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveCoroutine = StartCoroutine(AsyncHandleMove(direction));
    }

    private IEnumerator AsyncHandleMove(Vector3 targetDirection)
    {
        transform.position += targetDirection * _speed * Time.deltaTime;
        yield return null;
    }

    private IEnumerator AsyncLookAtForTarget(Vector3 targetDirection)
    {
        transform.rotation = Quaternion.LookRotation(targetDirection - transform.position);
        yield return null;
    }
}
