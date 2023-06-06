using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private Vector2 _parrallaxRatio;

    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;

    private float _textureUnitSizeY;

    private void Start()
    {
        StartParrallax();
    }

    private void StartParrallax()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position;

        Sprite s = GetComponent<SpriteRenderer>().sprite;
        Texture2D t = s.texture;

        _textureUnitSizeY = t.height / s.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMove = _mainCamTrm.position - _lastCamPos;
        transform.Translate(new Vector3(deltaMove.x * _parrallaxRatio.x, deltaMove.y * _parrallaxRatio.y),
            Space.World);
        _lastCamPos = _mainCamTrm.position;

        if (Mathf.Abs(_mainCamTrm.position.y - transform.position.y) >= _textureUnitSizeY)
        {
            float offsetY = (_mainCamTrm.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(_mainCamTrm.position.y - offsetY, transform.position.y);
        }
    }
}
