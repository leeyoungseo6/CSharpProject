using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite[] _sprite;

    private void Start()
    {
        int x = Random.Range(0, 4);
        _spriteRenderer.sprite = _sprite[x];
    }
}
