using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandlesPuzzle : MonoBehaviour
{
    [Header("Candle State")]
    public bool isLit = false;
    //private SpriteRenderer candleSprite;

    [Header("References")]
    [SerializeField] public GameObject candleFire;
    [SerializeField] private CandlePuzzleManager puzzleManager;
    [SerializeField] private Lighter lighter;
    [SerializeField] public Light2D candleLight;

    [Header("Interaction Settings")]
    [SerializeField] public float interactionDistance = 1.0f;

    private void Awake()
    {
        if (candleFire != null) { candleFire.SetActive(false); }

        if (candleLight != null) { candleLight.enabled = false;}

        //candleLight.gameObject.SetActive(false);
        candleLight.enabled = false;
        candleFire.SetActive(false);
    }

    private void Start()
    {
        lighter = GameObject.FindWithTag("Player").GetComponent<Lighter>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryToLightCandle();
        }
    }

    void TryToLightCandle()
    {
        if (Vector2.Distance(transform.position, lighter.transform.position) > interactionDistance)
        {
            return;
        }

        if (isLit) return;
        if (lighter == null || !lighter.hasLighter) return;

        if (lighter.hasLighter && !isLit)
        { 
            isLit = true;

            if (candleFire != null)
                candleFire.SetActive(true);

            if (candleLight != null)
                candleLight.enabled = true;

            if (puzzleManager != null)
                puzzleManager.CandleLit(this);
        }
    }
   
}
