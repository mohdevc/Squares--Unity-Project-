using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] float _movementSpeed = 10f;
    [SerializeField] int _hitDamage = 20;
    [SerializeField] Image _playerHealthbar;
    [SerializeField] GameObject _gameOver;
    [SerializeField] InputAction _rightMovement;
    [SerializeField] InputAction _leftMovement;

    public float _maxPlayerHealth = 100;
    public float _currentPlayerHealth;
    #endregion

    void Awake()
    {
        _gameOver.SetActive(false);
    }
    void OnEnable()
    {
        _rightMovement.Enable();
        _leftMovement.Enable();
    }
    void Start()
    {
        _currentPlayerHealth = _maxPlayerHealth;
    }
    void Update()
    {
        Die();
    }
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (_rightMovement.IsPressed())
        {
            transform.Translate(Vector2.right * _movementSpeed * Time.deltaTime);
        }
        else if (_leftMovement.IsPressed())
        {
            transform.Translate(Vector2.left * _movementSpeed * Time.deltaTime);
        }

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _currentPlayerHealth -= _hitDamage;
            UpdatePlayerHealthbar();
        }

    }

    void UpdatePlayerHealthbar()
    {
        _playerHealthbar.fillAmount = _currentPlayerHealth / _maxPlayerHealth;


        switch (_currentPlayerHealth)
        {
            case 100f:
                _playerHealthbar.color = Color.green;
                break;
            case 80f:
                _playerHealthbar.color = Color.yellow;
                break;
            case 60f:
                _playerHealthbar.color =
                GetComponent<SpriteRenderer>().color = new Color(1f, 0.647f, 0f);
                break;
            case 40f:
                _playerHealthbar.color = Color.red;
                break;
        }
    }
    void Die()
    {
        if (_currentPlayerHealth <= 0)
        {
            Destroy(gameObject);
            _gameOver.SetActive(true);
        }
    }
}
