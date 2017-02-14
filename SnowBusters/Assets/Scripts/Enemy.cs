using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    [SerializeField]
    Transform GoalPosition;
    NavMeshAgent agent;
    [SerializeField]
    int HP;
    Animator Anim;
    AudioSource AS;
	// Use this for initialization
	void Start () {
        AS = gameObject.GetComponent<AudioSource>();
        Anim = gameObject.GetComponent<Animator>();
        GoalPosition = GameObject.Find("Goal").transform;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = GoalPosition.position;
	}
	void Update()
    {
        Anim.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
    public void DMG(int dmg)
    {
        AS.Play();
        Anim.Play("takeDamage");
        Debug.Log("Damege");
        HP -= dmg;
        if (HP <= 0)
        {
            Anim.SetBool("Alive", false);
        }
    }
    public void Kill()
    {
        Destroy(gameObject);
    }
    void Disable()
    {
        AS.Play();
    }
}
