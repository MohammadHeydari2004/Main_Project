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

    void Start()
    {
       
    }

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
            transform.localScale = new Vector3(1, 1, 1); // رو به راست
        }
        else if (_Horizontal_Input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // رو به چپ
        }
    }

    private void Jump()
    {
        bool isGrounded = Physics2D.OverlapCircle(_GroundPosition.position, 0.3f, _LayerMask);

        //Debug.Log("Is _CountJump: " + _CountJump);
        //Debug.Log("Is Grounded: " + isGrounded);
        if (isGrounded)
        {
            _CountJump = 0; // تعداد پرش‌ها را صفر کنید
        }
        //Debug.Log("Is _CountJump: " + _CountJump);
        if (Input.GetKeyDown(KeyCode.Space) && _CountJump < _MaxJump)
        {
            //Debug.Log("Is _CountJump: " + _CountJump);
            _CountJump++;
            _Rigidbody.linearVelocity= new Vector2(_Rigidbody.linearVelocity.x, _JumpForse);
            _animator.SetTrigger("Main_Jump");
            //Debug.Log("Is _CountJump: " + _CountJump);
        }
    }
}