using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private PlayerSO _playerSO;

    [SerializeField]
    private LayerMask Star;
    private Collider2D coll;

    private int _orbitDir;

    private void Awake()
    {
        _speed = _playerSO.speed;
    }

    private void Update()
    {
        OrbitStar();
        PlayerMove();
    }

    private void PlayerMove()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void OrbitStar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            coll = Physics2D.OverlapCircle(transform.position, 6f, Star);
            if (coll == null) return;
            _orbitDir = coll.transform.position.x < 0 ? 180 : 0;
        }
        if (Input.GetKey(KeyCode.Space) && coll != null)
        {
            float z = Mathf.Atan2(coll.transform.position.y - transform.position.y,
                coll.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, z + _orbitDir);
        }
    }
}
