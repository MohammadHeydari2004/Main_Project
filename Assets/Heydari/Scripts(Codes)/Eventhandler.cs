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

    //public void OnHeavyAttackStart1() { bossAI.OnHeavyAttackStart(); }
    //public void OnHeavyAttackEnd1() { bossAI.OnHeavyAttackEnd(); }
    //public void OnIdleAnimationEnd1() { bossAI.OnIdleAnimationEnd(); }
    //public void OnQuickAttackStart1() { bossAI.OnQuickAttackStart(); }
    //public void OnQuickAttackEnd1() { bossAI.OnQuickAttackEnd(); }
    //public void OnVulnerableExit1() { bossAI.OnVulnerableExit(); }

    public void BossHealth1() { bossHealth.OnDeathAnimationEnd(); }

}
