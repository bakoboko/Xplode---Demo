using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool isPaused;

    public int score;
 
    public float time;
    public Text collectableUpdate;
    public Text deathCollectables;
    public Text deathTime, deathRestart, winRestart;
    public Text finalCollectables;
    public Text finalTime;
    public Text finalScore;
    public Text multiplierText;
    public TrailRenderer trail;

    private float total, restartTimer;
    private float totalCounter;
    private GameObject menu;
    private GameObject deathMenu;
    private GameObject pauseMenu;
    private GameObject pauseButton;
    private int currentScene;
    private bool multiply, setTimer;

    [SerializeField]
    private int countDown;


    void Start ()
    {
        isPaused = false;
        menu = GameObject.Find("ScoreMenu");
        deathMenu = GameObject.Find("DeathMenu");
        pauseMenu = GameObject.Find("PauseMenu");
        pauseButton = GameObject.Find("PauseButton");
        currentScene = SceneManager.GetActiveScene().buildIndex;
        trail = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<TrailRenderer>();
        trail.enabled = true;
        deathMenu.SetActive(false);
       // pauseMenu.SetActive(false);
        totalCounter = 0;

        Time.timeScale = 1;
        time = 0;
        score = 0;
    }
	

	void Update ()
    {
        TextDisplayUpdate();
        DeathMenu();
        ScoreUpdate();
        TimeUpdate();
    }

    void TimeUpdate()
    {
        if(EndZoneScript.hasFinished == false || PlayerCharacterScript.damage < 2)
        {
            time += Time.deltaTime;
        }
        restartTimer += Time.fixedDeltaTime;
    }

    void ScoreUpdate()
    {
        if(EndZoneScript.hasFinished == false)
        {
            total = score * 5 - time * 10;
            menu.SetActive(false);
        }
        else
        {
            Pause();
            menu.SetActive(true);
         //   pauseButton.SetActive(false);
            trail.enabled = false;
            RestartTime(winRestart);
          //  if (timeTick > 1)
            //{
              //  Time.timeScale -= 0.01f;
                //timeTick -= 1;
            //}

            if(Time.timeScale <= 0.1)
            {
                if (PlayerCharacterScript.damage == 0 && multiply == false)
                {
                    total *= 2;
                    multiplierText.text = "No Damage Reward! x2 Multiplier!";
                    multiply = true;
                    
                }

                totalCounter = total;
             /*   if (totalCounter != total)
                {
                    total = Mathf.Round(total);
                    if (totalCounter < total)
                    {
                        totalCounter++;
                        totalCounter += 20;
                    }
                    else if (totalCounter > total)
                    {
                        totalCounter--;
                    }
                }*/
            
            }
        }
    }

    void TextDisplayUpdate()
    {   
        collectableUpdate.text = "Stars: " + score;
        deathCollectables.text = "Collected: " + score + " Stars";
        deathTime.text = "Time Survived: " + time.ToString("F3") + " Seconds";
        finalCollectables.text = "Collected: " + score + " Stars";
        finalTime.text = "Time Taken: " + time.ToString("F3") + " Seconds";
        finalScore.text = "Final Score: " + totalCounter.ToString("F0")+ " points";
    }

    void DeathMenu()
    {

        if (PlayerCharacterScript.damage >= 2 && EndZoneScript.hasFinished == false)
        {
            Pause();
            deathMenu.SetActive(true);
           // pauseButton.SetActive(false);
            trail.enabled = false;

         
            RestartTime(deathRestart);

        }
    }

    void RestartTime(Text restart)
    {    
            if (!setTimer)
            {
                restartTimer = 0;
                setTimer = true;
            }

            if (countDown - Mathf.Round(restartTimer) > 0)
                restart.text = ("Restarting In: " + (countDown - Mathf.Round(restartTimer)));
            else
                Replay();
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuReturn()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        // if (PlayerCharacterScript.damage < 2 && EndZoneScript.hasFinished != true)
        //{
        isPaused = true;
            Time.timeScale = 0;
          //  pauseMenu.SetActive(true);
            //pauseButton.SetActive(false);
            trail.enabled = false;
       // }
    }

    public void Continue()
    {
      
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        trail.enabled = true;


    }
}
