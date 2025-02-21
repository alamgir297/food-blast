using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private float _spawnRate = 1.5f;
    private bool _isGameOver;
    private int _scoreGlobal;


    [SerializeField] private List<GameObject> _target;
    [SerializeField] private GameObject _targetContainer;
    [SerializeField] private TextMeshProUGUI _scoreUiText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _titleScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateScore(int score) {
        _scoreGlobal += score;
        _scoreUiText.text = "Score: " + _scoreGlobal;
    }
    public void GameOver() {
        _isGameOver = true;
        if (_gameOverText == null) {
            Debug.LogError("game over text not found");
        }
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }
    public bool IsGameOver() {
        return _isGameOver ? true : false;
    }

    public void StartGame(float diffLevel) {
        _spawnRate /=diffLevel;
        _isGameOver = false;
        _scoreGlobal = 0;
        _titleScreen.SetActive(false);
        StartCoroutine(SpawnTargetCoroutine());
        UpdateScore(_scoreGlobal);
    }
    public void RastartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator SpawnTargetCoroutine() {
        while (!_isGameOver) {
            yield return new WaitForSeconds(_spawnRate);
            int randIndex = Random.Range(0, _target.Count);
            GameObject item = Instantiate(_target[randIndex]);
            item.transform.parent = _targetContainer.transform;
        }
    }
}
