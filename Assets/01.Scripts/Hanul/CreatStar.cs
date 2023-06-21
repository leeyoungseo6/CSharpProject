using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatStar : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    Transform _player;

    int _prevscore = 0;
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
            if(GameManager.instance.Score > _prevscore + Random.Range(1, 2f))
            {
                _prevscore = GameManager.instance.Score;
                float x = Random.Range(0.7f, 1.4f);
                yield return new WaitForSeconds(x);
                GameObject enemy = Instantiate(_enemyPrefab);
                int rdIndex = Random.Range(0, transform.childCount);
                enemy.transform.position = transform.GetChild(rdIndex).position;
            }
            yield return null;
        }
    }

}
