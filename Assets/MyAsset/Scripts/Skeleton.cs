using UnityEngine;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _moveLeft;
    [SerializeField] private Vector3 _moveRight;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private Text _uiText;
    [SerializeField] private Animator _animator;
    private string _jumpKey = "Jump";
    private string _startKey = "StartAnimation";
    private float _score;
    private Vector3 _jumpPower = new Vector3(0, 8, 0);

    private bool PressLeftKey => Input.GetKeyDown(KeyCode.LeftArrow);
    private bool PressRightKey => Input.GetKeyDown(KeyCode.RightArrow);
    private bool PressSpaceKey => Input.GetKeyDown(KeyCode.Space);
    private bool PressYKey => Input.GetKeyDown(KeyCode.Y);

    private void Update()
    {
        GetScore();
        Jump();
        Move();
        EnableBodyAnimation();
    }

    private void GetScore()
    {
        _score += Time.deltaTime;
        _uiText.text = "Очки: " + Mathf.FloorToInt(_score);
    }

    private void EnableBodyAnimation()
    {
        if (PressYKey)
            _animator.SetTrigger(_startKey);
    }

    private void JumpAnimation() => _animator.SetTrigger(_jumpKey);

    private void Jump()
    {
        if (PressSpaceKey)
        {
            JumpAnimation();
            ApplyForceForMove(_jumpPower);
        }
    }

    private void Move()
    {
        if (PressLeftKey)
            ApplyForceForMove(_moveLeft);
        else if (PressRightKey)
            ApplyForceForMove((_moveRight));
    }

    private void ApplyForceForMove(Vector3 direction) => _rigidbody.AddForce(direction, ForceMode.Impulse);
}
