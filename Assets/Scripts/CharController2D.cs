using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController2D : MonoBehaviour
{

    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;

    public Animator animator;
    public Transform attackPoint;

    public float attackRange = 0.5f;
    private int attackDamage = 20;
    public LayerMask enemyLayers;

    private GameObject enemy;
    public GameObject Prefab;

    private void Start()
    {

        Instantiate(Prefab);

        animator = gameObject.GetComponent<Animator>();

        InvokeRepeating("Attack", 1f, 1f);  //1s delay, repeat every 1s

    }

    void FindEnemy()
    {
        enemy = GameObject.Find("Enemy(Clone)");
    }

    void Attack()
    {
        FindEnemy();

        if(enemy != null)
        {
            if (enemy.GetComponent<Enemy>().currentHealth > 0)
            {
                animator.SetTrigger("Attack");

                Debug.Log(attackDamage);

                //Collects all object that circle hits.
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                //Damage
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
                }

            }
        }
    }

    public void CreateEnemy()
    {
        StartCoroutine(WaitToCreate(1f));
    }

    private IEnumerator WaitToCreate(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Instantiate(Prefab);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
