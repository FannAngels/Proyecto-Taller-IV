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

    [SerializeField]public Light2D candleLight; // Drag your child light object in the inspector

    private void Awake()
    {
        candleSprite = GetComponent<SpriteRenderer>();
        candleFire.SetActive(false); // Make sure it's off at start
    }

    private void OnMouseDown()
    {
        if (Vector2.Distance(transform.position, lighter.transform.position) > 2f) return;

        if (lighter.hasLighter && !isLit)
        {
            isLit = true;
            candleFire.SetActive(true); // Enable the visual
            puzzleManager.CheckPuzzle();
        }
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
        Lighter inventory = GameObject.FindWithTag("Player").GetComponent<Lighter>();

        if (inventory.hasLighter && !isLit)
        {
            isLit = true;
            candleLight.enabled = true;
            puzzleManager.CheckPuzzle();
        }
    }
   
}
