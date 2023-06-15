using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatStar : MonoBehaviour
{
    [SerializeField] Transform _player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(0, _player.position.y);
    }
}
