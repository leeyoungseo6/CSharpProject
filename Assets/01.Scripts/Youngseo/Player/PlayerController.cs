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
    private Transform _lopeTrm;
    private Vector3 _startVec;
    private Transform _playerTrm;

    private float _orbitDir;

    private bool _startOrbit = false;

    #region Å×½ºÆ® UI
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private int _score = 0;
    private int _currentScore = 0;
    #endregion

    private void Awake()
    {
        _speed = _playerSO.speed;
        _playerTrm = transform.Find("PlayerVisual");
        _lopeTrm = transform.Find("Lope");
    }

    private void Update()
    {
        PlayerMove();
        OrbitStar();
        ScoreText();
    }

    private void PlayerMove()
    {
        _playerTrm.position += _playerTrm.up * _speed * Time.deltaTime;
    }

    private void OrbitStar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _starTrm = Physics2D.OverlapCircle(_playerTrm.position, 6f, Star).transform;
            if (_starTrm == null) return;
            _starTrm.position += new Vector3(0, 0, 0.1f);
            _startVec = _playerTrm.position;
        }
        if (Input.GetKey(KeyCode.Space) && _starTrm != null)
        {
            IsRightTri();
            if (_startOrbit)
            {
                float z = Mathf.Atan2(_starTrm.position.y - _playerTrm.position.y, _starTrm.position.x - _playerTrm.position.x) * Mathf.Rad2Deg;
                _playerTrm.rotation = Quaternion.Euler(0, 0, z + _orbitDir);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _lopeTrm.localScale = new Vector3(0, 0, 0);
            _startOrbit = false;
            _starTrm = null;
        }
    }

    private void IsRightTri()
    {
        float a = Vector3.Distance(_startVec, _playerTrm.position);
        float b = Vector3.Distance(_starTrm.position, _playerTrm.position);
        float c = Vector3.Distance(_starTrm.position, _startVec);
        float p = Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2);
        SetLope(b);
        if (p > 0 && !_startOrbit)
        {
            _startOrbit = true;
            int x = _starTrm.position.x < _playerTrm.position.x ? 1 : -1;
            int y = _playerTrm.up.normalized.y > 0 ? 1 : -1;
            _orbitDir = x * y == 1 ? 181: -1;
            _orbitDir += Time.deltaTime * _speed * x * y;
        }
    }

    private void SetLope(float length)
    {
        float z = Mathf.Atan2(_starTrm.position.y - _lopeTrm.position.y, _starTrm.position.x - _lopeTrm.position.x) * Mathf.Rad2Deg;
        _lopeTrm.rotation = Quaternion.Euler(0, 0, z);
        _lopeTrm.localScale = new Vector3(length, 1);
        _lopeTrm.position = _playerTrm.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void ScoreText()
    {
        _score = (int)((_playerTrm.position.y - transform.position.y) / 2);

        if (_currentScore < _score)
        {
            _scoreText.text = _score.ToString();
            _currentScore = _score;
        }
    }
}
