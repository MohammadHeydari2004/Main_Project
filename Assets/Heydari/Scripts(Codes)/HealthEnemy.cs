using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private Animator animator;

    private int currentHealth;
    private bool isDead = false;
    [SerializeField] string sceneName;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;


        currentHealth -= damageAmount;

      //  Debug.Log("health E: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;

        animator.SetTrigger("Die E");
    }

    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(sceneName);
    }
}