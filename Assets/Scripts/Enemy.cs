using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;
    private GameObject Hero;
    void Start()
    {
        Hero = GameObject.Find("Bandit");

        animator = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;

        Singleton.Instance.EnemyHandler += Instance_EnemyHandler;
    }

    private void Instance_EnemyHandler(object sender, EnemyDiedArgs e)
    {
        Singleton.Instance.EnemyState = e.IsEnemyAlive;
    }

    public void TakeDamage(int damage)
    {

        EnemyDiedArgs args = new EnemyDiedArgs();
     
        currentHealth -= damage;

        StartCoroutine(HurtAnim(0.3f));

        if (currentHealth <= 0)
        {
            args.IsEnemyAlive = false;
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);

        StartCoroutine(DeadAnim(1f));

        if(!Singleton.Instance.EnemyState)
        {
            Hero.GetComponent<CharController2D>().CreateEnemy();
        }
    }

    private IEnumerator DeadAnim(float seconds)
    {
        
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);

    }

    private IEnumerator HurtAnim(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetTrigger("Hurt");

    }


}
