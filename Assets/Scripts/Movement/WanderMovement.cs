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
        WaitForSeconds delay = new WaitForSeconds(Random.Range(2.5f, 4f));
        WaitForSeconds pause = new WaitForSeconds(Random.Range(3f, 4f));

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
