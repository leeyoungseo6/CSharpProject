using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatStar : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    Transform _player;
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine("SpawnEnemy");
    }
    private void Update()
    {
        transform.position = new Vector2(0, _player.position.y);
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
