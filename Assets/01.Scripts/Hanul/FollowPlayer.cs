using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;


    void Start()
    {

    }

    void Update()
    {
        transform.position = new Vector2(0, _player.position.y);
    }
}
