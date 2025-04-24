using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] LayerMask _LayerMask;
    [SerializeField] Transform _GroundPosition;

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(_GroundPosition.position, 0.3f, _LayerMask);
        if (Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            _animator.SetTrigger("Main_Attack_Combo");
        }
        else if (Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            _animator.SetTrigger("Main_Attack");
        }

    }
}
