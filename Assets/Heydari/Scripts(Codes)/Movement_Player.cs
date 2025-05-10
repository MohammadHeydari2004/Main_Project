using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    [SerializeField] float _Main_Move_Speed = 5f;
    [SerializeField] float _JumpForse = 10f;
    [SerializeField] LayerMask _LayerMask;
    [SerializeField] Transform _GroundPosition;
    [SerializeField] Animator _animator;
    [SerializeField] int _MaxJump = 2;
    [SerializeField] Rigidbody2D _Rigidbody;
    private float _Horizontal_Input;
    private int _CountJump = 0;
    private bool _isGroundedPrev = false;

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        _Horizontal_Input = Input.GetAxis("Horizontal");
        _Rigidbody.linearVelocity = new Vector2(_Horizontal_Input * _Main_Move_Speed, _Rigidbody.linearVelocity.y);

        _animator.SetFloat("Main_Blend", Mathf.Abs(_Horizontal_Input));

        if (_Horizontal_Input > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Right
        }
        else if (_Horizontal_Input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Left
        }
    }

    private void Jump()
    {
        bool isGrounded = Physics2D.OverlapCircle(_GroundPosition.position, 0.8f, _LayerMask);

        // Check if just landed
        if (isGrounded && !_isGroundedPrev)
        {
            _animator.SetInteger("Is_Jump", 0);
            _CountJump = 0;
        }

        _isGroundedPrev = isGrounded;

        if (Input.GetKeyDown(KeyCode.Space) && _CountJump < _MaxJump)
        {
            _Rigidbody.linearVelocity = new Vector2(_Rigidbody.linearVelocity.x, _JumpForse);
            _animator.SetTrigger("Main_Jump");
            _animator.SetInteger("Is_Jump", 1);
            _CountJump++;
        }
    }
}