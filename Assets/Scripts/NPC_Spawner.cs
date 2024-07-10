using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawner : MonoBehaviour
{
    [SerializeField] private float SpawnDelay;
    [SerializeField] private float SpawnRange;
    [SerializeField] private int MaxSpawn;
    [SerializeField] private NPC womam, men;

    private List<NPC> list = new();

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(SpawnDelay);

        while (true)//wait, that's illegal!
        {
            if(list.Count < MaxSpawn)
            {
                NPC target = Random.Range(0, 2) == 0 ? womam : men;

                Vector3 spawnPoint = Vector3.up * 2.266f;
                spawnPoint.x = Random.Range(-SpawnRange, SpawnRange);
                spawnPoint.z = Random.Range(-SpawnRange, SpawnRange);
                spawnPoint += transform.position;

                NPC instance = Instantiate(target, spawnPoint, Quaternion.identity);
                list.Add(instance);
            }
            yield return delay;
            CleanList();
        }
    }

    private void CleanList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list.RemoveAt(i);
                i--;
            }
        }
    }
}
