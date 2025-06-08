using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private List<Transform> _points=new List<Transform>();
    [SerializeField] private float _minDistanceToTarget = 0.05f;
    [SerializeField] private float _speed;
   
    //private float _distanceForPlayer = 4f;
    private Player _player;
    private Coroutine _coroutine;

    private Vector3 _currentPoint;
    public Queue<Vector3> PointMove { get; set; }

    private void Awake()
    { 
      PushPointForMove();
        _player = FindObjectOfType<Player>();
    }

    private void PushPointForMove()
    {  
        PointMove = new Queue<Vector3>();
        foreach (var point in _points)
        {
            PointMove.Enqueue(point.position);
        }
    }

    private void SwitchPoint()
    {
        _currentPoint = PointMove.Dequeue();
        PointMove.Enqueue(_currentPoint);
    }

    private void LoopRoute()
    {
        Vector3 direction = _currentPoint - transform.position;

        if (direction.magnitude <= _minDistanceToTarget)
        {
            SwitchPoint();
        }
        Vector3 normalizedDirection = direction.normalized;

        ProcessMoveTo(normalizedDirection);
    }
    private void Update()
    {
        LoopRoute();

        if (_player.IsSafetyZone)
            LookAtForTarget(_player.transform.position);
        else
            LookAtForTarget(_currentPoint);
    }

    private void ProcessMoveTo(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    private void LookAtForTarget(Vector3 targetDirection)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(AsyncLookAtForTarget(targetDirection));
    }

    private IEnumerator AsyncLookAtForTarget(Vector3 targetDirection)
    {
        transform.rotation = Quaternion.LookRotation(targetDirection - transform.position);
        yield return null;
    }
}
