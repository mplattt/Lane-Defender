using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    [SerializeField] private Transform point3;
    [SerializeField] private Transform point4;
    [SerializeField] private Transform point5;
    [SerializeField] private GameObject lightEnemy;
    [SerializeField] private GameObject mediumEnemy;
    [SerializeField] private GameObject heavyEnemy;
    private Transform point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        int spawnPoint = Random.Range(0, 5);
        int enemy = Random.Range(0, 3);
        if (spawnPoint == 0)
        {
            point = point1;
        }
        else if (spawnPoint == 1)
        {
            point = point2;
        }
        else if (spawnPoint == 2)
        {
            point = point3;
        }
        else if (spawnPoint == 3)
        {
            point = point4;
        }
        else if (spawnPoint == 4)
        {
            point = point5;
        }

        if (enemy == 0)
        {
            Instantiate(lightEnemy, new Vector2(point.transform.position.x, point.transform.position.y),
                Quaternion.identity);
        }
        else if (enemy == 1)
        {
            Instantiate(mediumEnemy, new Vector2(point.transform.position.x, point.transform.position.y),
                Quaternion.identity);
        }
        else if (enemy == 2)
        {
            Instantiate(heavyEnemy, new Vector2(point.transform.position.x, point.transform.position.y),
                Quaternion.identity);
        }

        yield return new WaitForSeconds(1.75f);
        StartCoroutine(Spawn());
    }
}
