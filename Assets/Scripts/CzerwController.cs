using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CzerwController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float arriveDistance = 0.5f;
    [SerializeField] private float danceSpeed = 1f;
    [SerializeField] private float maxDance = 6f;
    [SerializeField] private float eatingSpeed = 1f;
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRend;
    [Header("Debug")]
    [SerializeField] private PlantController goal = null;
    [SerializeField] private bool dancing;

    [Header("Read Only")]
    [SerializeField] private float danceProgress = 0;

    // ---------- Unity messages

    private void Update()
    {
        if (!dancing)//not dancing
        {
            if (!GoalReached() && danceProgress <= 0f)//going for lebiodka
            {
                if (goal == null)
                {
                    if (FarmController.Instance.GetClosestPlant(transform.position, out goal) == false)
                    {
                        UpdateAnimator();
                        return;
                    }
                    else
                    {
                        //TODO - assign plant (and listen for gathering/death)
                    }
                }

                transform.position = Vector3.MoveTowards(transform.position, goal.transform.position, moveSpeed * Time.deltaTime);

                //rotate sprite
                if (transform.position.x > goal.transform.position.x)
                    spriteRend.transform.eulerAngles = Vector3.up * 180;
                else
                    spriteRend.transform.eulerAngles = Vector3.zero;
            }
            else if (goal != null)//eating lebiodka
            {
                goal.EatPlant(eatingSpeed * Time.deltaTime);
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

    private bool GoalReached()
    {
        if (goal != null)
        {
            return Vector3.Distance(transform.position, goal.transform.position) <= arriveDistance;
        }
        else
            return false;
    }

    private void UpdateAnimator()
    {
        if (!GoalReached())
            animator.SetFloat("Speed", 1f);
        else
            animator.SetFloat("Speed", 0f);

        animator.SetFloat("Dance", danceProgress);
    }

}
