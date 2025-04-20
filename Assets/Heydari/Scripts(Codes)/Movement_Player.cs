using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    [SerializeField] float _Main_Move_Speed;
    [SerializeField] Animator _animator;
    [SerializeField] float _Horizontal_Input;

    [SerializeField] Transform _GroundPsition;
    [SerializeField] LayerMask _LayerMask;
    [SerializeField] float _JumpForse;
    [SerializeField] float _MaxJump = 2;
    private float _CountJump = 0;
    private Rigidbody2D _Rigidbody;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        _Horizontal_Input = Input.GetAxisRaw("Horizontal");
        Vector3 movement_Right = new(_Horizontal_Input, 0, 0);

        _animator.SetFloat("Blend", 0);

        if (_Horizontal_Input == 1)
        {
            // حرکت به راست
            transform.rotation = Quaternion.Euler(0, 0, 0); // رو به راست
            transform.Translate(_Main_Move_Speed * Time.deltaTime * movement_Right);
            _animator.SetFloat("Blend", 0.25f);
        }
        else if (_Horizontal_Input == -1f)
        {
            // حرکت به چپ
            Vector3 movement_Left = new(-(_Horizontal_Input), 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0); // رو به چپ
            transform.Translate(_Main_Move_Speed * Time.deltaTime * movement_Left);
            _animator.SetFloat("Blend", -0.25f);
        }

    }
    private void Jump()
    {
        if (Physics2D.OverlapCircle(_GroundPsition.position, 0.01f, _LayerMask) == true)
        {
            _CountJump = 1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _Rigidbody.linearVelocity = new(_Rigidbody.linearVelocity.x, _JumpForse);
                _animator.SetFloat("Blend", 1f);
                //print(_Rigidbody.linearVelocity.ToString());
            }
        }
        else if (_CountJump < _MaxJump && Input.GetKeyDown(KeyCode.Space))
        {
            _CountJump++;
            _Rigidbody.linearVelocity = new(_Rigidbody.linearVelocity.x, _JumpForse);
            _animator.SetFloat("Blend", 1f);
        }
    }
}
