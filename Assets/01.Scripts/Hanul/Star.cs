using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    SpriteRenderer _speiteRenderer;
    [SerializeField] Sprite[] _sprite;
    void Start()
    {
        int x = Random.Range(0, 3);
        _speiteRenderer.sprite = _sprite[x];
        _speiteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
