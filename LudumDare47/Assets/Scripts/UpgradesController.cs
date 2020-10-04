using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private int startingCurrency = 0;
    [SerializeField] private TMP_Text currencyText = null;
    [SerializeField] private string currencyString = "Total Blobs = "; //<O> + <C> = <T>"
    [SerializeField] private PlayerStats player = null;
    [SerializeField] private TMP_Text sizeText = null;
    [SerializeField] private string sizeString = "Loop Size: ";
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private TMP_Text speedText = null;
    [SerializeField] private string speedString = "Speed: ";
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private TMP_Text spawnRateText = null;
    [SerializeField] private string spawnRateString = "Spawn Rate: ";
    [SerializeField] private TMP_Text blobSizeText = null;
    [SerializeField] private string blobSizeString = "Blob Size: ";

    private GameController gameController;
    private Spawner spawner;

    private int oldCurrency = 0;
    private int currentCurrency = 0;
    private int score = 0;

    private float currentSize = 1;
    private float currentSpeed = 1f;
    private float currentSpawnRate = 1f;
    private float currentBlobSize = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        spawner = Spawner.instance;

        currentCurrency = startingCurrency;
    }

    public void UpdateUI(int _score)
    {
        oldCurrency = currentCurrency;
        score = _score;
        currentCurrency += _score;
        currencyText.text = currencyString + oldCurrency + " + " + _score + " = " + currentCurrency;
    }

    public void IncreaseSize()
    {
        // shift click
        if(Input.GetKey(KeyCode.LeftShift) && !CheckCost(10))
        {
            if(currentSize + 1f > maxSize) return;
            currentCurrency -= 10;
            currentSize += 1f;
        }
        else if(Input.GetKey(KeyCode.LeftControl) && !CheckCost(100)) // crtl click
        {
            if(currentSize + 10f > maxSize) return;
            currentCurrency -= 100;
            currentSize += 10f;
        }
        else if(!CheckCost(1)) // one
        {
            if(currentSize > maxSize) return;
            currentCurrency--;
            currentSize += 0.1f;
        }

        player.UpdateSize(currentSize);
        sizeText.text = sizeString + Mathf.Round(currentSize*100f)/100f + " / " + maxSize;
        UpdateUIAfterPurchase();
    }

    public void IncreaseSpeed()
    {
        // shift click
        if(Input.GetKey(KeyCode.LeftShift) && !CheckCost(10))
        {
            if(currentSpeed + 1f > maxSpeed) return;
            currentCurrency -= 10;
            currentSpeed += 1f;
        }
        else if(Input.GetKey(KeyCode.LeftControl) && !CheckCost(100)) // crtl click
        {
            if(currentSpeed + 10f > maxSpeed) return;
            currentCurrency -= 100;
            currentSpeed += 10f;
        }
        else if(!CheckCost(1)) // one
        {
            if(currentSpeed > maxSpeed) return;
            currentCurrency--;
            currentSpeed += 0.1f;
        }

        player.UpdateSpeed(currentSpeed);
        speedText.text = speedString + Mathf.Round(currentSpeed*100f)/100f + " / " + maxSpeed;
        UpdateUIAfterPurchase();
    }

    public void IncreaseSpawnRate()
    {
        // shift click
        if(Input.GetKey(KeyCode.LeftShift) && !CheckCost(10))
        {
            currentCurrency -= 10;
            currentSpawnRate += 1f;
        }
        else if(Input.GetKey(KeyCode.LeftControl) && !CheckCost(100)) // crtl click
        {
            currentCurrency -= 100;
            currentSpawnRate += 10f;
        }
        else if(!CheckCost(1)) // one
        {
            currentCurrency--;
            currentSpawnRate += 0.1f;
        }

        spawner.SetSpawnRate(currentSpawnRate);
        spawnRateText.text = spawnRateString + Mathf.Round(currentSpawnRate*100f)/100f;
        UpdateUIAfterPurchase();
    }

    // returns true if negative after subtracting i
    private bool CheckCost(int i)
    {
        return (currentCurrency - i) < 0;
    }

    private void UpdateUIAfterPurchase()
    {
        currencyText.text = currencyString + oldCurrency + " + " + score + " = " + currentCurrency;
    }


}
