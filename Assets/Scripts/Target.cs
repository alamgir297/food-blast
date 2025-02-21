using UnityEngine;

public class Target : MonoBehaviour {
    private float _sideBound = 4.5f;
    private float _lowerBound = -4f;
    private float _torqueLimit = 15f;


    private Rigidbody _targetRb;
    private BoxCollider _targetBc;
    private GameManager _gameManager;

    [SerializeField] private int _objectScore;
    [SerializeField] private float _upwardForceLimit;
    [SerializeField] private ParticleSystem _particleEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _targetRb = GetComponent<Rigidbody>();
        _targetBc = GetComponent<BoxCollider>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //_score = 0;

        TossUpward();
        GenerateSpawnPosition();
    }

    // Update is called once per frame
    void Update() {
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
            Destroy(gameObject, 0.1f);
        }
    }
    private void CheckLowerBound() {
        if (transform.position.y <= _lowerBound) {
            if (gameObject.CompareTag("Good")) {
                _gameManager.GameOver();
                Debug.Log("overman");
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
