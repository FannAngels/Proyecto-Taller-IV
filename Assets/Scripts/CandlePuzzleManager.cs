using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
    public CandlesPuzzle[] candles;
    public GameObject door;
    private int failedAttempts = 0;
    public Enemy enemy; // Reference to the enemy script

    [Header("Order Settings")]
    public int currentIndex = 0;

    public void CandleLit (CandlesPuzzle candle)
    {
        int candleIndex = System.Array.IndexOf(candles, candle);
        Debug.Log($"Candle Index{candleIndex}");

        if (candleIndex == currentIndex)
        {
            currentIndex++;

            if (currentIndex >= candles.Length)
            {
                door.SetActive(false);
                Debug.Log("Puzzle Solved!");
            }
        }
        else
        {
            failedAttempts++;
            enemy.IncreaseDifficulty(failedAttempts);
            ResetPuzzle();
        }
    }

    public void ResetPuzzle()
    {
        Debug.Log("Wrong order! Puzzle reset.");

        foreach (CandlesPuzzle c in candles)
        {
            c.isLit = false;
            if (c.candleFire != null) c.candleFire.SetActive(false);
            if (c.candleLight != null) c.candleLight.enabled = false;
        }

        currentIndex = 0;
    }

    /*public bool IsCorrectOrder()
    {
        // OPTIONAL: implement if you want ordered puzzle
        return true;
    }*/

    
}