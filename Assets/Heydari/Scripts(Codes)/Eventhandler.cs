using UnityEngine;
using UnityEngine.Events;

public class Eventhandler : MonoBehaviour
{
    public UnityEvent unityEvent;
    public BossAI bossAI;
    public BossHealth bossHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void startattack()
    {

    }
    public void startevent()
    {
        unityEvent.Invoke();
    }

    public void BossHealth1() { bossHealth.OnDeathAnimationEnd(); }

}
