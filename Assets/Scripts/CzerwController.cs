using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CzerwController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float arriveDistance = 0.5f;
    [SerializeField] private float danceSpeed = 1f;
    [SerializeField] private float maxDance = 6f;
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRend;
    [Header("Debug")]
    [SerializeField] private Transform goal;
    [SerializeField] private bool dancing;

    [Header("Read Only")]
    [SerializeField] private bool reached = false;
    [SerializeField] private float danceProgress = 0;

    // ---------- Unity messages

    private void Update()
    {
        if (!dancing)//not dancing
        {
            if (!reached && danceProgress <= 0f)//going for lebiodka
            {
                transform.position = Vector3.MoveTowards(transform.position, goal.position, moveSpeed * Time.deltaTime);

                //rotate sprite
                if (transform.position.x > goal.position.x)
                    spriteRend.transform.eulerAngles = Vector3.up * 180;
                else
                    spriteRend.transform.eulerAngles = Vector3.zero;

                if (Vector3.Distance(transform.position, goal.position) <= arriveDistance)
                {
                    reached = true;
                }
            }
            else//eating lebiodka
            {

            }

            if (danceProgress > 0f)
            {
                danceProgress -= danceSpeed * Time.deltaTime;
            }
        }
        else//dancing
        {
            danceProgress += danceSpeed * Time.deltaTime;
        }

        UpdateAnimator();
    }

    // ---------- private methods

    private void UpdateAnimator()
    {
        if (!reached)
            animator.SetFloat("Speed", 1f);
        else
            animator.SetFloat("Speed", 0f);

        animator.SetFloat("Dance", danceProgress);
    }

}
