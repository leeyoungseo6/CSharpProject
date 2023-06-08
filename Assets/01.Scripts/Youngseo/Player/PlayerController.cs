using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float _speed;

    [SerializeField]
    private PlayerSO _playerSO;

    [SerializeField]
    private LayerMask Star;
    private Transform _starTrm;
    [SerializeField]
    private Transform _ropeTrm;
    private Vector3 _startVec;

    private float _orbitDir;

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
        OrbitStar();
        ScoreText();
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
                float z = Mathf.Atan2(_starTrm.position.y - transform.position.y, _starTrm.position.x - transform.position.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, z + _orbitDir);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _walls[0].color = new Color(0.8f, 0.4f, 0.4f);
            _walls[1].color = new Color(0.8f, 0.4f, 0.4f);
            
            _ropeTrm.localScale = new Vector3(0, 0, 0);
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
            _walls[0].color = new Color(0.6f, 0.8f, 0.8f);
            _walls[1].color = new Color(0.6f, 0.8f, 0.8f);

            _startOrbit = true;
            int x = _starTrm.position.x < transform.position.x ? 1 : -1;
            int y = transform.up.normalized.y > 0 ? 1 : -1;
            _orbitDir = x * y == 1 ? 181: -1;
            _orbitDir += Time.deltaTime * _speed * x * y;
        }
    }

    private void SetLope(float length)
    {
        float z = Mathf.Atan2(_starTrm.position.y - _ropeTrm.position.y, _starTrm.position.x - _ropeTrm.position.x) * Mathf.Rad2Deg;
        _ropeTrm.rotation = Quaternion.Euler(0, 0, z);
        _ropeTrm.localScale = new Vector3(length, 1);
        _ropeTrm.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") && !_startOrbit)
        {
            Destroy(gameObject);
        }
    }

    private void ScoreText()
    {
        _score = (int)((transform.position.y - _gameStartPos.y) / 2);

        if (_currentScore < _score)
        {
            _scoreText.text = $"{_currentScore:D2}";
            _currentScore = _score;
        }
    }
}
