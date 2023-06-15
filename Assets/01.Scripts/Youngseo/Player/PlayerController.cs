using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private PlayerSO _playerSO;

    [SerializeField]
    private LayerMask Star;
    private Collider2D[] _starCollsL;
    private Collider2D[] _starCollsR;
    private Vector3 _targetStar;
    [SerializeField]
    private Transform _ropeTrm;
    private Vector3 _startVec;
    private float _toStarDis;
    private bool _left;

    private float _orbitDir = 0;

    private bool _startOrbit = false;

    [SerializeField]
    SpriteRenderer[] _walls;

    #region Å×½ºÆ® UI
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _score = 0;
    private int _currentScore = 0;
    Vector3 _gameStartPos;
    #endregion

    private void Awake()
    {
        _speed = _playerSO.speed;
        _gameStartPos = transform.position;
    }

    private void Update()
    {
        PlayerMove();
        SetScoreText();
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
            Targeting();
            _startVec = transform.position;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (IsRightTri())
            {
                float angle = Mathf.Atan2(_targetStar.y - transform.position.y, _targetStar.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle + _orbitDir); 
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _walls[0].color = new Color(0.8f, 0.4f, 0.4f);
            _walls[1].color = new Color(0.8f, 0.4f, 0.4f);

            _ropeTrm.localScale = Vector3.zero;
            _startOrbit = false;
            _left = false;
        }
    }

    private void Targeting()
    {
        _toStarDis = 100;
        _starCollsL = Physics2D.OverlapBoxAll(transform.position + transform.right * -4, new Vector2(8f, 18f), transform.rotation.z, Star);
        _starCollsR = Physics2D.OverlapBoxAll(transform.position + transform.right *  4, new Vector2(8f, 18f), transform.rotation.z, Star);

        foreach (Collider2D target in _starCollsL)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < _toStarDis)
            {
                _toStarDis = distance;
                _targetStar = target.transform.position;
                _left = true;
            }
        }

        foreach (Collider2D target in _starCollsR)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance < _toStarDis)
            {
                _toStarDis = distance;
                _targetStar = target.transform.position;
                _left = false;
            }
        }
        _ropeTrm.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.right * -4, new Vector2(8f, 18f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.right *  4, new Vector2(8f, 18f));
    }

    private bool IsRightTri()
    {
        float a = Vector3.Distance(_startVec, transform.position);
        float b = Vector3.Distance(_targetStar, transform.position);
        float c = Vector3.Distance(_targetStar, _startVec);
        float p = Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2);
        SetRope(b);
        if (p > 0 && !_startOrbit)
        {
            _walls[0].color = new Color(0.6f, 0.8f, 0.8f);
            _walls[1].color = new Color(0.6f, 0.8f, 0.8f);

            _startOrbit = true;
            if (_left) _orbitDir = 180 + 2.8f / b;
            else _orbitDir = -2.8f / b;
        }
        return _startOrbit;
    }

    private void SetRope(float length)
    {
        _ropeTrm.right = (_targetStar - transform.position).normalized;
        _ropeTrm.localScale = new Vector3(length, 1);
        _ropeTrm.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && !_startOrbit)
        {
            Destroy(_ropeTrm.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Star"))
        {
            Destroy(_ropeTrm.gameObject);
            Destroy(gameObject);
        }
    }

    private void SetScoreText()
    {
        _score = (int)((transform.position.y - _gameStartPos.y) / 2);

        if (_currentScore < _score)
        {
            _scoreText.text = $"{_currentScore:D2}";
            _currentScore = _score;
        }
    }
}
