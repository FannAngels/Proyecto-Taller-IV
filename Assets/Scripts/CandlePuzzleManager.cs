using UnityEngine;

public class CandlePuzzleManager : MonoBehaviour
{
    public CandlesPuzzle[] candles;
    public GameObject door;
    private int failedAttempts = 0;
    public Enemy enemy; // Reference to the enemy script

    public void CheckPuzzle()
    {
        int litCount = 0;

        foreach (CandlesPuzzle c in candles)
        {
            if (c.isLit) litCount++;
        }

        if (litCount == candles.Length)
        {
            door.SetActive(false); // Door opens
        }
        else if (litCount > 0 && !IsCorrectOrder()) // Optional fail condition
        {
            failedAttempts++;
            enemy.IncreaseDifficulty(failedAttempts);
            ResetPuzzle();
        }
    }

    public bool IsCorrectOrder()
    {
        // OPTIONAL: implement if you want ordered puzzle
        return true;
    }

    public void ResetPuzzle()
    {
        foreach (CandlesPuzzle c in candles)
        {
            c.isLit = false;
            // Reset sprite here if needed
        }
    }
}