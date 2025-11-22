
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] disabledObjects;
    [SerializeField] GameObject[] mainObjects;
    [SerializeField] TextMeshProUGUI _highScoreUI;

    [SerializeField] TextMeshProUGUI _timer;
    int[] _newScore = new int[2], _highScoreArray = new int[2];
    int _minutes, _seconds;
    string _highScore;
    float _elapsedTime;
    Player _player;
    bool _enableTimer = false;
    void Start()
    {
        ShowHighScore();
        _player = GameObject.Find("Player").GetComponent<Player>();
        foreach (GameObject obj in mainObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(false);
        }
    }
    void Update()
    {
        if (_enableTimer == true)
        {
            Timer();
        }

        if (_player._currentPlayerHealth <= 0)
        {
            _enableTimer = false;
            Invoke("GameOver", 2f);
        }
    }
    public void StartGame()
    {
        foreach (GameObject obj in mainObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in disabledObjects)
        {
            obj.SetActive(true);
            _enableTimer = true;
        }
        _player._maxPlayerHealth = 100;
    }
    public void Timer()
    {
        _elapsedTime += Time.deltaTime;
        _minutes = Mathf.FloorToInt(_elapsedTime / 60);
        _seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _timer.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        _newScore[0] = _minutes; _newScore[1] = _seconds;
    }

    void GameOver()
    {
        int _oldMinutes = PlayerPrefs.GetInt("minutes", 0);
        int _oldSeconds = PlayerPrefs.GetInt("seconds", 0);

        if (_newScore[0] > _oldMinutes || (_newScore[0] == _oldMinutes && _newScore[1] > _oldSeconds))
        {
            PlayerPrefs.SetInt("minutes", _newScore[0]);
            PlayerPrefs.SetInt("seconds", _newScore[1]);
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("GamePlay");
    }
    void ShowHighScore()
    {
        _highScoreArray[0] = PlayerPrefs.GetInt("minutes", 0);
        _highScoreArray[1] = PlayerPrefs.GetInt("seconds", 0);
        _highScore = string.Format("{0:00}:{1:00}", _highScoreArray[0], _highScoreArray[1]);
        _highScoreUI.text = $"High Score: {_highScore}";
    }

}
