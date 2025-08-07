using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    private List<Transform> pieces;
    private Vector2Int dimensions;
    private float width;
    private float height;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Texture2D texture in imageTexture)
        {
            Image image = Instantiate(levelSelectPrefab, levelSelectPanel);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            image.GetComponent<Button>().onClick.AddListener(delegate { StartGame(texture); });

            
        }
    }

    // Update is called once per frame
    public void StartGame(Texture2D jigsawTexture)
    {
        levelSelectPanel.gameObject.SetActive(false);

        // List for each jigsaw piece
        pieces = new List<Transform>();
        Debug.Log(pieces.Count);

        //Calculate size of jigsaw pieces based on difficulty 
        dimensions = GetDimensions(jigsawTexture, difficulty);

        CreateJigsawPieces(jigsawTexture);
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

                Scatter();
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
}
