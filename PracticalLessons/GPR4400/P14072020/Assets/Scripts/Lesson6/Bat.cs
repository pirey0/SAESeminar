using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] float detectDistance;
    [SerializeField] float speed;
    [SerializeField] private bool dead;

    private BatState currentState = BatState.Idle;

    Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TryTransitionState();
        UpdateState();
    }

    private void TryTransitionState()
    {
        switch (currentState)
        {
            case BatState.Idle:
                if (TargetInRange())
                    TransitionTo(BatState.Chasing);
                break;
        }

        if(currentState != BatState.Dead && dead)
        {
            TransitionTo(BatState.Dead);
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case BatState.Chasing:
                ChasingUpdate();
                break;
        }

    }

    private void ChasingUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * Time.deltaTime * speed;
        transform.localScale = direction.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
        Debug.Log("Hello");
    }

    private void TransitionTo(BatState newstate)
    {
        switch (newstate)
        {
            case BatState.Idle:
                animator.SetTrigger("Idle");
                break;

            case BatState.Dead:
                OnDeadEnter();
                break;

            case BatState.Chasing:
                animator.SetTrigger("Chasing");
                break;
        }
        currentState = newstate;
        Debug.Log("Transition to: " + newstate);
    }

    private void OnDeadEnter()
    {
        animator.SetTrigger("Dead");
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<Rigidbody2D>();
    }

    private bool TargetInRange()
    {
        return Vector3.Distance(transform.position, target.position) < detectDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Kill Bat"))
        {
            dead = true;
        }
    }

}

public enum BatState
{
    Idle,
    Chasing,
    Dead
}
