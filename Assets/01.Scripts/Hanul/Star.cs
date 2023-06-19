using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] Sprite[] _sprite;
    SpriteRenderer _speiteRenderer;
    void Start()
    {
        _speiteRenderer = GetComponent<SpriteRenderer>();
        int x = Random.Range(0, 3);
        _speiteRenderer.sprite = _sprite[x];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
