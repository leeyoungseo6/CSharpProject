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
    private Transform _starTrm;

    private int _orbitDir;

    private bool _startOrbit = false;

    private Vector3 _startVec;

    private void Awake()
    {
        _speed = _playerSO.speed;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void LateUpdate()
    {
        OrbitStar();
    }

    private void PlayerMove()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void OrbitStar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _starTrm = Physics2D.OverlapCircle(transform.position, 6f, Star).transform;
            if (_starTrm == null) return;
            _starTrm.position += new Vector3(0, 0, 0.1f);
            _startVec = transform.position;
        }
        if (Input.GetKey(KeyCode.Space) && _starTrm != null)
        {
            IsRightTri();
            if (_startOrbit)
            {
                float z = Mathf.Atan2(_starTrm.position.y - transform.position.y,
                    _starTrm.position.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, z + _orbitDir);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _startOrbit = false;
            _starTrm = null;
        }
    }

    private void IsRightTri()
    {
        float a = Vector3.Distance(_startVec, transform.position);
        float b = Vector3.Distance(_starTrm.position, transform.position);
        float c = Vector3.Distance(_starTrm.position, _startVec);
        float p = Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2);
        if (p > 0 && !_startOrbit)
        {
            _startOrbit = true;
            int x = _starTrm.position.x < transform.position.x ? 1 : -1;
            int y = transform.up.normalized.y > 0 ? 1 : -1;
            _orbitDir = x * y == 1 ? 181 : -1;
        }
    }
}
