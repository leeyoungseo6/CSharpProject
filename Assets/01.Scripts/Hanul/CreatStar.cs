using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatStar : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    void Start()
    {
        
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if(!Input.GetKey(KeyCode.Space))
            {

                float x = Random.Range(0.7f, 1.4f);
                yield return new WaitForSeconds(x);
                GameObject enemy = Instantiate(enemyPrefab);
                int rdIndex = Random.Range(0, transform.childCount);
                enemy.transform.position = transform.GetChild(rdIndex).position;
            }
            yield return null;
        }
    }


}
