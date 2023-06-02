using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private PlayerSO _playerSO;
    private Rigidbody2D _rigid;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        _rigid.velocity = new Vector2(x, 0) * _playerSO.speed;
    }

    private void LateUpdate()
    {
        _rigid.velocity = new Vector2(Mathf.Clamp(_rigid.velocity.x, -2f, 2f), _rigid.velocity.y);
    }
}
