using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class JigsawManager : MonoBehaviour
{
    [Header("Puzzle Elements")]
    [Range(2, 6)]
    [SerializeField] private int difficulty = 4;
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;

    [Header("UI Elements")]
    [SerializeField] private List<Texture2D> imageTexture;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Image levelSelectPrefab;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private GameObject playAgainButton2;

    private List<Transform> pieces;
    private Vector2Int dimensions;
    private float width;
    private float height;

    private Transform draggingPiece = null;
    private Vector3 offset;

    private int piecesCorrect;

    public WinCondition winCondition;

    int imageIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imageTexture.


        for (int i = 0; i < imageTexture.Count; i++)
        {
            Image image = Instantiate(levelSelectPrefab, levelSelectPanel);
            image.sprite = Sprite.Create(imageTexture[i], new Rect(0, 0, imageTexture[i].width, imageTexture[i].height), Vector2.zero);

            image.GetComponent<Button>().onClick.AddListener(delegate { StartGame(imageTexture[i], i + 1); });
        }

        /*foreach (Texture2D texture in imageTexture)
        {
            Image image = Instantiate(levelSelectPrefab, levelSelectPanel);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            image.GetComponent<Button>().onClick.AddListener(delegate { StartGame(texture); });

            
        }*/
    }

    // Update is called once per frame
    public void StartGame(Texture2D jigsawTexture, int puzzle)
    {
        levelSelectPanel.gameObject.SetActive(false);

        // List for each jigsaw piece
        pieces = new List<Transform>();
        Debug.Log(pieces.Count);

        //Calculate size of jigsaw pieces based on difficulty 
        dimensions = GetDimensions(jigsawTexture, difficulty);

        CreateJigsawPieces(jigsawTexture);

        Scatter();

        UpdateBorder();

        piecesCorrect = 0;

        imageIndex = puzzle;
    }

    Vector2Int GetDimensions(Texture2D jigsawTexture, int difficulty)
    {
        Vector2Int dimensions = Vector2Int.zero;
        if (jigsawTexture.width < jigsawTexture.height)
        {
            dimensions.x = difficulty;
            dimensions.y = (difficulty * jigsawTexture.height) / jigsawTexture.width;

        }
        else
        {
            dimensions.x = (difficulty * jigsawTexture.width) / jigsawTexture.height;
            dimensions.y = difficulty;
        }
        return dimensions;
    }

    void CreateJigsawPieces(Texture2D jigsawTexture)
    {
        height = 1f / dimensions.y;
        float aspect = (float)jigsawTexture.width / jigsawTexture.height;
        width = aspect / dimensions.x;

        for (int row = 0; row < dimensions.y; row++)
        {
            for (int col = 0; col < dimensions.x; col++)
            {

                Transform piece = Instantiate(piecePrefab, gameHolder);
                piece.localPosition = new Vector3(
                    (-width * dimensions.x / 2) + (width * col) + (width / 2),
                    (-height * dimensions.y / 2) + (height * row) + (height / 2)
                    - 1);
                piece.localScale = new Vector3(width, height, 1f);

                piece.name = $"Piece {(row * dimensions.x) + col}";
                Debug.Log($"{piece.name} adding to pieces next line");
                pieces.Add(piece);
                Debug.Log(pieces.Count);


                float width1 = 1f / dimensions.x;
                float height1 = 1f / dimensions.y;

                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2(width1 * col, height1 * row);
                uv[1] = new Vector2(width1 * (col + 1), height1 * row);
                uv[2] = new Vector2(width1 * col, height1 * (row + 1));
                uv[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));

                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;

                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);
            }
        }
    }

    private void Scatter()
    {
        float ortoHeight = Camera.main.orthographicSize;
        float screenAspect = (float)Screen.width / Screen.height;
        float ortoWidth = (screenAspect * ortoHeight);

        float pieceWidth = width * gameHolder.localScale.x;
        float pieceHeight = height * gameHolder.localScale.y;

        ortoHeight -= pieceHeight;
        ortoWidth -= pieceWidth;

        foreach (Transform piece in pieces)
        {
            float x = Random.Range(-ortoWidth, ortoWidth);
            float y = Random.Range(-ortoHeight, ortoHeight);

            piece.position = new Vector3(x, y, -1);

        }
    }

    private void UpdateBorder()
    {
        LineRenderer lineRenderer = gameHolder.GetComponent<LineRenderer>();

        float halfWidth = (width * dimensions.x) / 2f;
        float halfHeight = (height * dimensions.y) / 2f;
        float borderZ = 0f;

        lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.enabled = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                draggingPiece = hit.transform;
                offset = draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }

        if (draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapAndDisableIfCorrect();
            draggingPiece.position += Vector3.forward;
            draggingPiece = null;
        }

        if (draggingPiece)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition += offset;
            draggingPiece.position = newPosition;
        }

        void SnapAndDisableIfCorrect()
        {
            int pieceIndex = pieces.IndexOf(draggingPiece);

            int col = pieceIndex % dimensions.x;
            int row = pieceIndex / dimensions.x;

            Vector2 targetPosition = new((-width * dimensions.x / 2) + (width * col) + (width / 2),
                                         (-height * dimensions.y / 2) + (height * row) + (height / 2));

            if (Vector2.Distance(draggingPiece.localPosition, targetPosition) < (width / 2))
            {
                draggingPiece.localPosition = targetPosition;

                draggingPiece.GetComponent<BoxCollider2D>().enabled = false;

                piecesCorrect++;

                if (piecesCorrect == pieces.Count)
                {
                    switch (imageIndex)
                    {
                        case 1:
                            playAgainButton.SetActive(true);
                            break;

                        case 2:
                            playAgainButton2.SetActive(true);
                            break;
                    }
                }
            }
        }
    }

    public void RestartGame(int puzzle)
    {
        foreach (Transform piece in pieces)
        {
            Destroy(piece.gameObject);
        }
        pieces.Clear();

        gameHolder.GetComponent<LineRenderer>().enabled = false;

        playAgainButton.SetActive(false);
        levelSelectPanel.gameObject.SetActive(true);

        winCondition.ImageDone(puzzle);

    }

    public void RestartGame2(int puzzle)
    {
        foreach (Transform piece in pieces)
        {
            Destroy(piece.gameObject);
        }
        pieces.Clear();

        gameHolder.GetComponent<LineRenderer>().enabled = false;

        playAgainButton.SetActive(false);
        levelSelectPanel.gameObject.SetActive(true);

        winCondition.ImageDone(puzzle);

    }

}
