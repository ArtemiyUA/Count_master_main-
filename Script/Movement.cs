using UnityEngine;
using System.Collections;

public abstract class Movement : MonoBehaviour
{
    private void Awake()
    {
        StartMovement();
    }
    protected bool isSurrounded = false;
    protected bool resumeMovement = true;
    private Coroutine resumeCoroutine;

    private IEnumerator ResumeMovementCoroutine()
    {
        yield return new WaitForSeconds(1f);

        if (resumeMovement && !isSurrounded)
        {
            StartMovement();
        }

        resumeCoroutine = null;
    }

    public void SetSurrounded(bool surrounded)
    {
        isSurrounded = surrounded;

        if (isSurrounded)
        {
            StopMovement();
        }
        else if (resumeMovement)
        {
            if (resumeCoroutine == null)
            {
                resumeCoroutine = StartCoroutine(ResumeMovementCoroutine());
            }
            else
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = StartCoroutine(ResumeMovementCoroutine());
            }
        }
    }

    public void SetResumeMovement(bool resume)
    {
        resumeMovement = resume;

        if (resumeMovement && !isSurrounded)
        {
            if (resumeCoroutine == null)
            {
                resumeCoroutine = StartCoroutine(ResumeMovementCoroutine());
            }
            else
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = StartCoroutine(ResumeMovementCoroutine());
            }
        }
        else
        {
            StopMovement();
            if (resumeCoroutine != null)
            {
                StopCoroutine(resumeCoroutine);
                resumeCoroutine = null;
            }
        }
    }
    public abstract void Move(Vector3 direction);
    public abstract void StopMovement();
    public abstract void StartMovement();
}
