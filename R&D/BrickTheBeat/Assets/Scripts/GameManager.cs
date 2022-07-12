using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject playerPrefab;
    public Text scoreText;
    public Text notesText;
    public Text levelText;
    public Text highscoreText;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompleted;
    public GameObject panelGameOver;

    public GameObject[] levels;

    public static int instrumentHit = 0;

    // Sets up the Instance static variable 
    public static GameManager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, LEVELCOMPLETED, LOADLEVEL, GAMEOVER }
    State _state;
    GameObject _currentNote;
    GameObject _currentLevel;

    bool _isSwitchingState;

    private int _score;

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = "SCORE: " + _score;
        }
    }

    private int _level;

    public int Level
    {
        get { return _level; }
        set { _level = value;
            levelText.text = "Level: " + _level;
        }
    }

    private int _notes;

    public int Notes
    {
        get { return _notes; }
        set
        {
            _notes = value;
            notesText.text = "Notes: " + _notes;
        }
    }

public void PlayClicked()
    {
        SwitchState(State.INIT);
    }

    void Start()
    {
        Instance = this;
        SwitchState(State.MENU);
        // Resets the Highscore
        //PlayerPrefs.DeleteKey("highscore");
    }

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        _isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        _state = newState;
        BeginState(newState);
        _isSwitchingState = false;
    }

    void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                Cursor.visible = true;
                highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Notes = 4;
                if (_currentLevel != null)
                {
                    Destroy(_currentLevel);
                }
                Instantiate(playerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                Destroy(_currentLevel);
                instrumentHit = 0;
                Level++;
                SwitchState(State.LOADLEVEL, 2f);
                break;
            case State.LOADLEVEL:
                if (Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else
                {
                    _currentLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                break;
            case State.GAMEOVER:
                instrumentHit = 0;
                if (Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                panelGameOver.SetActive(true);
                break;
        }
    }

    void Update()
    {
        switch (_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if (_currentNote == null)
                {
                    if (Notes > 0)
                    {
                        _currentNote = Instantiate(notePrefab);
                    }
                    else
                    {
                        SwitchState(State.GAMEOVER);
                    }
                }
                // Controls the success factors for completing levels per level
                if (Level == 0)
                {
                    if (instrumentHit == 10 && !_isSwitchingState)
                    {
                        Cursor.visible = true;
                        panelLevelCompleted.SetActive(true);
                        if (Input.anyKeyDown)
                        {
                            SwitchState(State.LEVELCOMPLETED);
                        }
                        break;
                    }
                }
                if (Level == 1)
                {
                    if (instrumentHit == 2 && !_isSwitchingState)
                    {
                        Cursor.visible = true;
                    Destroy(_currentNote);
                    //panelLevelCompleted.SetActive(true);
                    if (Input.anyKeyDown)
                    {
                        SwitchState(State.LEVELCOMPLETED);
                    }
                    break;
                    }
                }
                break;
            case State.LEVELCOMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if (Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    void EndState()
    {
        switch (_state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.LEVELCOMPLETED:
                panelLevelCompleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }
}
