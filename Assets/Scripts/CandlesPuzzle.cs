using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandlesPuzzle : MonoBehaviour
{
    public bool isLit = false;
    private SpriteRenderer candleSprite;

    [Header("References")]
    public GameObject candleFire;
    private CandlePuzzleManager puzzleManager;
    private Lighter lighter;

    public Light2D candleLight; // Drag your child light object in the inspector

    private void Awake()
    {
        candleLight.gameObject.SetActive(false);
        //candleLight.enabled = false;
        candleFire.SetActive(false); // Make sure it's off at start
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                TryToLightCandle();
            }
        }
    }

    void TryToLightCandle()
    {
        if (isLit) return;
        if (lighter == null || !lighter.hasLighter) return;
        if (Vector2.Distance(transform.position, lighter.transform.position) > 2f) return;

        isLit = true;

        if (candleFire != null)
            candleFire.SetActive(true);

        if (candleLight != null)
            candleLight.enabled = true;

        if (puzzleManager != null)
            puzzleManager.CheckPuzzle();
    }
   
}
