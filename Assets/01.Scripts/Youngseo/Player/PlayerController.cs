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
    private Transform _lopeTrm;
    private Vector3 _startVec;

    private int _orbitDir;

    private bool _startOrbit = false;


    private void Awake()
    {
        _speed = _playerSO.speed;
        _lopeTrm = transform.Find("Lope");
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
                Vector2 toStar = (_starTrm.position - transform.position).normalized;
                transform.right = toStar * _orbitDir;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _lopeTrm.localScale = new Vector3(0, 0, 0);
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
        SetLope(b);
        if (p > 0 && !_startOrbit)
        {
            _startOrbit = true;
            int x = _starTrm.position.x < transform.position.x ? 1 : -1;
            int y = transform.up.normalized.y > 0 ? 1 : -1;
            _orbitDir = x * y == 1 ? -1 : 1;
        }
    }

    private void SetLope(float length)
    {
        float z = Mathf.Atan2(_starTrm.position.y - _lopeTrm.position.y, _starTrm.position.x - _lopeTrm.position.x) * Mathf.Rad2Deg;
        _lopeTrm.rotation = Quaternion.Euler(0, 0, z + _orbitDir);
        _lopeTrm.localScale = new Vector3(length, 1, 1);
    }
}
