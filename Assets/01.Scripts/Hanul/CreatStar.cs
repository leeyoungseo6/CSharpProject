using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreatStar : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    Transform _player;
    private int _starCount;
    int _prevscore = 0;

    void Start()
    {
        _starCount = 0;
        _player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine("SpawnEnemy");
    }
    private void Update()
    {
        if (GameManager.instance._gameOver) return;
        transform.position = new Vector2(0, _player.position.y);
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if(GameManager.instance.Score > _prevscore + 3f)
            {
                _prevscore = GameManager.instance.Score;
                int rdIndex = Random.Range(0, transform.childCount);
                if (_starCount <= 5 && rdIndex == 0) rdIndex = 3;
                Instantiate(_enemyPrefab, transform.GetChild(rdIndex).position, Quaternion.identity);
                _starCount++;
            }
            yield return null;
        }
    }

}
