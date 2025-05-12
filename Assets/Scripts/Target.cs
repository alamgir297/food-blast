using UnityEngine;
using TMPro;

public class Target : MonoBehaviour {
    private float _sideBound = 4.5f;
    private float _lowerBound = -4f;
    private float _torqueLimit = 15f;
    private Vector3 _popupOfset = new(0, 3, 0);


    private Rigidbody _targetRb;
    private BoxCollider _targetBc;
    private GameManager _gameManager;
    private TextMeshPro _popupText;
    private AudioManager _audioController;

    [SerializeField] private int _objectScore;
    [SerializeField] private float _upwardForceLimit;
    [SerializeField] private ParticleSystem _particleEffect;
    [SerializeField] private GameObject _scoreTextPopup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _targetRb = GetComponent<Rigidbody>();
        _targetBc = GetComponent<BoxCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _audioController = GetComponent<AudioManager>();
        //_score = 0;

        TossUpward();
        GenerateSpawnPosition();
    }

    // Update is called once per frame
    void FixedUpdate() {
        ToggleCollider();
        CheckLowerBound();
    }

    void TossUpward() {
        _targetRb.AddForce(Vector3.up * GenerateForce(), ForceMode.Impulse);
        _targetRb.AddTorque(GenerateTorque(), GenerateTorque(), GenerateTorque(), ForceMode.Force);
    }

    float GenerateForce() {
        return Random.Range(10, _upwardForceLimit);
    }
    void GenerateSpawnPosition() {
        float pointX = Random.Range(-_sideBound, _sideBound);
        transform.position = new Vector3(pointX, -2, 0);
    }
    float GenerateTorque() {
        return Random.Range(-_torqueLimit, _torqueLimit);
    }

    // colliders and physics
    void ToggleCollider() {
        if (transform.position.y >= -1.2f) {
            _targetBc.enabled = true;
        }
        else {
            _targetBc.enabled = false;
        }
    }

    private void OnMouseDown() {
        if (!_gameManager.IsGameOver()) {
            _gameManager.UpdateScore(_objectScore);
           
            Instantiate(_particleEffect, transform.position, Quaternion.identity);
            
            GameObject popup= Instantiate(_scoreTextPopup, transform.position , Quaternion.identity);
            _popupText = popup.GetComponent<TextMeshPro>();
            _popupText.text = "" + _objectScore;
            if (gameObject.CompareTag("Bad")) {
                int live = _gameManager.Lives;
                live--;
                _gameManager.UpdateLives(live);
                _popupText.color=Color.red;
                _audioController.PlayBlastSound();
                if (_gameManager.Lives < 1) {
                    _audioController.PlayBlastSound();
                    _gameManager.GameOver();
                }
            }
            else {
                _popupText.color = Color.green;
                _audioController.PlayPopSound();
            }

            Destroy(gameObject, 0.1f);
            Destroy(popup, 0.3f);
        }
    }
    private void CheckLowerBound() {
        if (transform.position.y <= _lowerBound) {
            if (gameObject.CompareTag("Good")) {
                _gameManager.UpdateScore(-2);
                Debug.Log("overman");
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
