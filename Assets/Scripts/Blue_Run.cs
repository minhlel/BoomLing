using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;


public class Blue_Run : StateMachineBehaviour
{   
    public float speed = 200f;
    Transform player;
    
    BlueScript blueScript;
    Rigidbody2D rb;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        blueScript = animator.GetComponent<BlueScript>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BlueScript>().LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }
}
