using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject[] trainBoxes;
    [SerializeField] GameObject trainTrack;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject titleScreen;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider volumeLevel;
    [SerializeField] List<AudioSource> goodSFX;
    [SerializeField] List<AudioSource> badSFX;

    public float boxIntervalDistance = 0.3f;
    public float currentVolume = 0.25f;
    public bool isGameActive = false;
    public bool isGamePaused = false;

    private int count = 0;
    private int maxTime = 60;
    private int currentTime;
    private bool readyToSpawn = false;

    private Vector3 trackEnd;
    private Vector3 trackCheckpoint;
    private AudioSource backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        volumeLevel.GetComponent<Slider>().value = currentVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive || isGamePaused)
        {
            SetVolume();
        }

        if (isGameActive && readyToSpawn)
        {
            SpawnTrainBox();
            readyToSpawn = false;
        }

        PauseToggle();
    }

    //Spawns a train car
    void SpawnTrainBox()
    {
        //Randomizes which box to spawn
        int boxIndex = Random.Range(0, trainBoxes.Length);
        Vector3 spawnPos = CalcSpawnPos(trainBoxes[boxIndex]);

        //Instantiates Box and sets it destination
        GameObject currentBox = Instantiate(trainBoxes[boxIndex], spawnPos,
            trainBoxes[boxIndex].transform.rotation);

        currentBox.GetComponent<TrainMovement>().SetDestination(trackEnd);
    }

    //Tracks gametime and updates timer display
     IEnumerator SetTimer()
    {
        for(int i = 0; i < maxTime; i++)
        {
            yield return new WaitForSeconds(1);
            currentTime--;
            timerText.text = "Time: " + currentTime;
        }//endfor
        GameOver();
    }

    //Updates the Counter
    public void UpdateCounter(int valueChange)
    {
        PlayRandomSound(valueChange);
        count = count + valueChange;
        counterText.text = "Count: " + count;
    }

    //To be used by checkpoint to inform the game manager that there is room to spawn the next train cart
    public void CheckpointReached()
    {
        readyToSpawn = true;
    }

    //Sets the position vectors from the train track
    public void SetTracks()
    {
        trackEnd = trainTrack.transform.GetChild(1).position;
        trackCheckpoint = trainTrack.transform.GetChild(2).position;
    }

    //Checks box size and adjusts the spawn position for consistent box spacing relative to the checkpoint
    Vector3 CalcSpawnPos(GameObject trainBox)
    {
        float boxWidth = Vector3.Distance(trainBox.transform.GetChild(1).position,
            trainBox.transform.GetChild(2).position);
        return Vector3.MoveTowards(trackCheckpoint, trackEnd, -(boxWidth/2 + boxIntervalDistance));
    }

    /// <summary>
    /// Game Control
    /// </summary>

    //Starts the game
    public void StartGame()
    {
        titleScreen.SetActive(false);
        timerText.gameObject.SetActive(true);
        counterText.gameObject.SetActive(true);
        volumeLevel.gameObject.SetActive(false);

        counterText.text = "Count: " + count;
        timerText.text = "Time: " + maxTime;
        currentTime = maxTime;
        isGameActive = true;
        isGamePaused = false;

        SetTracks();
        StartCoroutine(SetTimer());
        SpawnTrainBox();
    }

    //Restarts the game
    public void RestartGame()
    {
        gameOverText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Triggers game over screen
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        readyToSpawn = false;
        isGameActive = false;
    }

    //Allows for pausing the game
    void PauseToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameActive)
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                Time.timeScale = 0.0f;
                backgroundMusic.Pause();
                pauseMenu.SetActive(true);
                volumeLevel.gameObject.SetActive(true);
            }//endif
            else
            {
                Time.timeScale = 1.0f;
                backgroundMusic.Play();
                pauseMenu.SetActive(false);
                volumeLevel.gameObject.SetActive(false);
            }//endelse
        }//endif
    }//end PauseToggle


    /// <summary>
    /// Sound Control
    /// </summary>
    /// 
    //Sets the background music volume
    void SetVolume()
    {
        currentVolume = volumeLevel.GetComponent<Slider>().value;
        backgroundMusic.volume = currentVolume;
    }

    //Selects a random good or bad sound to play on point loss/gain
    void PlayRandomSound(int valueChange)
    {
        if(valueChange > 0)
        {
            int randSFX = Random.Range(0, goodSFX.Count);
            goodSFX[randSFX].Play();
        }
        else
        {
            int randSFX = Random.Range(0, badSFX.Count);
            badSFX[randSFX].Play();
        }
    }

}
