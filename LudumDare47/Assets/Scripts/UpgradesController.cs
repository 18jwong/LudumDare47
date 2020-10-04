using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText = null;
    [SerializeField] private string currencyString = "Total Blobs = "; //<O> + <C> = <T>"
    [SerializeField] private PlayerStats player = null;
    [SerializeField] private TMP_Text sizeText = null;
    [SerializeField] private string sizeString = "Loop Size: ";
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private TMP_Text speedText = null;
    [SerializeField] private string speedString = "Speed: ";
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
        if(currentSize > maxSize) return;
        if(CheckCost(1)) return;
        currentCurrency--;

        currentSize += 0.1f;
        player.UpdateSize(currentSize);
        sizeText.text = sizeString + Mathf.Round(currentSize*100f)/100f + " / " + maxSize;
        UpdateUIAfterPurchase();
    }

    public void IncreaseSpeed()
    {
        if(CheckCost(1)) return;
        currentCurrency--;

        currentSpeed += 0.1f;
        player.UpdateSpeed(currentSpeed);
        speedText.text = speedString + Mathf.Round(currentSpeed*100f)/100f;
        UpdateUIAfterPurchase();
    }

    public void IncreaseSpawnRate()
    {
        if(CheckCost(1)) return;
        currentCurrency--;

        currentSpawnRate += 0.1f;
        spawner.AddToSpawnRate(0.1f);
        spawnRateText.text = spawnRateString + Mathf.Round(currentSpawnRate*100f)/100f;
        UpdateUIAfterPurchase();
    }

    private bool CheckCost(int i)
    {
        return (currentCurrency - i) < 0;
    }

    private void UpdateUIAfterPurchase()
    {
        currencyText.text = currencyString + oldCurrency + " + " + score + " = " + currentCurrency;
    }


}
