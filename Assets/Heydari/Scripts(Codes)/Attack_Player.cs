using System.Collections;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Transform _groundPosition;
    [SerializeField] private Collider2D weaponCollider; 
    [SerializeField] private float attackDuration = 0.2f; // زمان فعال بودن کولایدر

    private void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundPosition.position, 0.3f, _groundLayerMask);

        if (Input.GetKeyDown(KeyCode.F) && isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _animator.SetTrigger("Main_Attack_Combo");
            }
            else
            {
                _animator.SetTrigger("Main_Attack");
            }
            ActivateWeaponCollider();
        }
    }

    private void ActivateWeaponCollider()
    {
        weaponCollider.enabled = true;
        StartCoroutine(DisableColliderAfterDelay());
    }

    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(attackDuration);
        weaponCollider.enabled = false;
    }

  

}
