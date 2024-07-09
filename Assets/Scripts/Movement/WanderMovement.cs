using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderMovement : MonoBehaviour
{
    [SerializeField] private Movement movement;

    private void Start()
    {
        StartWander();
    }

    public void StartWander()
    {
        StartCoroutine(Wander());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Wander()
    {
        WaitForSeconds delay = new WaitForSeconds(2.5f);
        WaitForSeconds pause = new WaitForSeconds(4);

        while (movement != null)
        {
            Vector3 dir = Vector3.zero;
            dir.x = Random.Range(-1f, 1f);
            dir.z = Random.Range(-1f, 1f);

            movement.ChangeDirection(dir.normalized);
            yield return delay;
            movement.ChangeDirection(Vector3.zero);
            yield return pause;
        }
    }
}
