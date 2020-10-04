using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_Text score = null;
    [SerializeField] private string scoreText = "People Collected: ";
    [SerializeField] private TMP_Text timer = null;
    [SerializeField] private string timerText = "Time Left: ";
    [SerializeField] private int startingSeconds = 30;
    [SerializeField] private TMP_Text three = null;
    [SerializeField] private TMP_Text two = null;
    [SerializeField] private TMP_Text one = null;
    [SerializeField] private int originalSize = 260;
    [SerializeField] private int shrinkRate = 10;
    [SerializeField] private GameObject upgradePanel = null;

    private UpgradesController upgradesController;
    private Spawner spawner;

    private int currentScore;
    private int currentSeconds = 30;

    public static GameController instance;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        upgradesController = GetComponent<UpgradesController>();
        spawner = GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = scoreText + currentScore;
    }

    public void Restart()
    {
        spawner.KillAllBlobs();
        currentScore = 0;
        upgradePanel.SetActive(false);
        currentSeconds = startingSeconds;
        timer.text = timerText + currentSeconds;
        StartCoroutine("ReduceTime");
        spawner.StartSpawning();
    }

    public void AddToScore(int i )
    {
        currentScore += i;
    }

    public void SetScore(int i)
    {
        currentScore = i;
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetCurrentSeconds()
    {
        return currentSeconds;
    }

    private IEnumerator ReduceTime()
    {
        while(currentSeconds > 0)
        {
            currentSeconds--;
            timer.text = timerText + currentSeconds;
            if(currentSeconds == 3)
            {
                StartCoroutine("ShrinkThree");
            }
            else if(currentSeconds == 2)
            {
                StartCoroutine("ShrinkTwo");
            }
            else if(currentSeconds == 1)
            {
                StartCoroutine("ShrinkOne");
            }

            yield return new WaitForSeconds(1f);
        }
        upgradePanel.SetActive(true);
        UpdateUpgradePanel();
    }

    private void UpdateUpgradePanel()
    {
        upgradesController.UpdateUI(GetScore());
    }

    private IEnumerator ShrinkThree()
    {
        three.gameObject.SetActive(true);
        three.fontSize = originalSize; 
        while (three.fontSize > 1)
        {
            three.fontSize -= shrinkRate;
            yield return new WaitForSeconds(.025f);
        }
        three.gameObject.SetActive(false);
    }

    private IEnumerator ShrinkTwo()
    {
        two.gameObject.SetActive(true);
        two.fontSize = originalSize; 
        while (two.fontSize > 1)
        {
            two.fontSize -= shrinkRate;
            yield return new WaitForSeconds(.025f);
        }
        two.gameObject.SetActive(false);
    }

    private IEnumerator ShrinkOne()
    {
        one.gameObject.SetActive(true);
        one.fontSize = originalSize; 
        while (one.fontSize > 1)
        {
            one.fontSize -= shrinkRate;
            yield return new WaitForSeconds(.025f);
        }
        one.gameObject.SetActive(false);
    }
}
