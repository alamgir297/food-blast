using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyButtons : MonoBehaviour {

    private Button _button;

    [SerializeField] private float _difficultyLevel;
    [SerializeField] GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetDifficulty() {
        _gameManager.StartGame(_difficultyLevel);
        Debug.Log("button pressed: "+ _button.gameObject.name);
    }
}
