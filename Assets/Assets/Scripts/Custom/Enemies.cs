using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    [SerializeField] private Animator[] animators;
    [SerializeField] public int damage = 1;
    [SerializeField] public int delay = 10;
    [SerializeField] private TextMeshProUGUI damageUI;
    [SerializeField] private TextMeshProUGUI timer_attack;
    [SerializeField] private Unit[] units;
    // Start is called before the first frame update

    private float remainingDelay;
    private bool isGameStarted = false;
    private bool isFinishedUpdate = true;
    void Start()
    {
        incDamage(0);
        remainingDelay = delay;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (isGameStarted && isFinishedUpdate)
        {
            remainingDelay -= Time.deltaTime;
            if (remainingDelay <= 0)
            {
                isFinishedUpdate = false;
                attack();

            }
            updAtkTimer(remainingDelay);

        }
        else remainingDelay = delay + Random.RandomRange(-2, 2);
    }

    public void attack() {
        foreach (var animator in animators)
        {
            StartCoroutine(animDelay(animator));
        }
        StartCoroutine(nextAttackCoolDown());
        var i = Random.Range(0, 2);
        units[i].addHpCount(-damage, i);
        incDamage(Mathf.Clamp(Random.Range(-2, 8), 0, 5));
    }

    IEnumerator animDelay(Animator animator)
    {
        float wait_time = Random.Range(0f, 1f);
        //Debug.Log(wait_time);
        yield return new WaitForSeconds(wait_time);
        animator.Play("Archer_Attack");

    }

    IEnumerator nextAttackCoolDown()
    {
        yield return new WaitForSeconds(2.5f);
        remainingDelay = delay - Random.Range(-3, 3);
        isFinishedUpdate=true;
    }

    public void incDamage(int amount)
    { 
        if(amount < 0)
        {
            damage = Mathf.Clamp(damage + amount, 2, damage);
            foreach (var unit in units)
            {
                unit.GetComponent<Animator>().Play("Attack");
            }
        } else
            damage += amount;
        damageUI.text = "Enemies: " + damage;
    }

    public void updAtkTimer(float value)
    {
        timer_attack.text = "Attack in: " + (value.ToString("F1")) + "s";
    }

    public void setIsStarted(bool newVal)
    {
        isGameStarted = newVal;
    }

    public void addDelay(float value)
    {
        remainingDelay += value * 2 + Random.Range(0.5f, 1f);
        updAtkTimer(delay);
    }

    public void setIdleAnim()
    {
        this.enabled = false;
        foreach (var animator in animators)
        {
            animator.Play("Archer_Idle");
        }
    }
}
