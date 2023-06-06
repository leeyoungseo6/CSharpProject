using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField]
    private float _parallaxRatio;

    private Transform _mainCamTrm;
    private float _lastCamY;

    private float _textureUnitSizeY;

    private void Start()
    {
        StartParrallax();
    }

    private void StartParrallax()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamY = _mainCamTrm.position.y;

        Sprite s = GetComponent<SpriteRenderer>().sprite;
        Texture2D t = s.texture;

        _textureUnitSizeY = t.width / s.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        float deltaMoveY = _mainCamTrm.position.y - _lastCamY;
        transform.Translate(new Vector3(0, deltaMoveY * _parallaxRatio),
            Space.World);
        _lastCamY = _mainCamTrm.position.y;

        if (Mathf.Abs(_mainCamTrm.position.y - transform.position.y) >= _textureUnitSizeY)
        {
            float offsetY = (_mainCamTrm.position.y - transform.position.y) % _textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, _mainCamTrm.position.y - offsetY);
        }
    }
}
