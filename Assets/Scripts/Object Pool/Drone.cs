using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Drone : MonoBehaviour
{
    public IObjectPool<Drone> Pool { get; set; }

    public float _currentHealth;

    [SerializeField]
    private float maxHealth = 100f;

    [SerializeField]
    private float timeToSelfDestruct = 3f;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        AttackPlayer();
        StartCoroutine(SelfDestruct());
    }

    private void OnDisable()
    {
        ResetDrone();
    }

    public void AttackPlayer()
    {
        Debug.Log("Attack Player");
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToSelfDestruct);
        TakeDamage(maxHealth);
    }

    private void ResetDrone()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if(_currentHealth <= 0f)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void Update()
    {

    }
}
