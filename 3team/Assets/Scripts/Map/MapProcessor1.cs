using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class MapProcessor1 : MonoBehaviour
{
    private Texture2D mapTexture;
    private int gridSizeX;
    private int gridSizeY;
    private float brightnessThreshold = 0.964444444444444f; // ��� ���� �������� �Ͼ�� �Ǻ��� ���� �Ӱ谪
    private NaverMapAPI _naverMapAPI;
    // �׸��� Ÿ���� ��Ÿ���� ������
    public enum GridType
    {
        Road,  // ���� ������ �νĵǴ� ��� ��
        Obstacle, // ��ο� ������ �νĵǴ� ��� ��ֹ�
    }

    // �̹����� 2�� �迭�� ��ȯ�� �׸���
    private GridType[,] grid;

    private Vector2 buttonsPos;
    public void Init(Vector2 userPosition, Vector2 buttonPosition)
    {
        _naverMapAPI = gameObject.GetComponent<NaverMapAPI>();

        mapTexture = _naverMapAPI.mapTexture;
        InitializeGrid();
        TextureClassification();
        buttonPosition = _naverMapAPI.Clamping(buttonPosition.x, buttonPosition.y);
        buttonsPos = buttonPosition;
        userPosition = _naverMapAPI.Clamping(userPosition.x, userPosition.y);
        StartCoroutine("StartProcessImage");

    }


    // �׸��� �ʱ�ȭ
    void InitializeGrid()
    {
        if (mapTexture != null)
        {
            gridSizeX = mapTexture.width;
            gridSizeY = mapTexture.height;
            grid = new GridType[gridSizeX, gridSizeY];
        }
        else
        {
            Debug.LogError("mapTexture�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
    // �̹��� ó�� �Լ�

    public List<Vector2Int> path { get; set; }
    public bool isProcessImage { get; set; }

    private void TextureClassification()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Color pixelColor = mapTexture.GetPixel(x, y);

                float brightness = (pixelColor.r + pixelColor.g + pixelColor.b) / 3f;

                if (brightness >= brightnessThreshold)
                {
                    grid[x, y] = GridType.Road;
                }
                else
                {
                    grid[x, y] = GridType.Obstacle;
                }
            }
        }
    }

    IEnumerator StartProcessImage()
    {
        while (true)
        {
            ProcessImage(Manager.UI.userPosition, buttonsPos);
            yield return new WaitForSeconds(3f);
        }
    }

    public Vector2Int userPos { get; set; }
    public Vector2Int buttonPos { get; set; }
    void ProcessImage(Vector2 userPosition, Vector2 buttonPosition)
    {

        if (mapTexture != null && grid != null)
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Dictionary<Vector2Int, Vector2Int> parentMap = new Dictionary<Vector2Int, Vector2Int>(); // ��Ʈ��ŷ�� ���� �θ� �����ϴ� ����

            userPos = FindClosestRoadCoordinate(userPosition);
            buttonPos = FindClosestRoadCoordinate(buttonPosition);

            queue.Enqueue(userPos);
            parentMap[userPos] = userPos;

            if (path != null)
            {
                foreach (Vector2Int pathPos in path)
                {
                    mapTexture.SetPixel(pathPos.x, pathPos.y, Color.white);
                }
            }
            path = new List<Vector2Int>();

            while (queue.Count > 0)
            {
                Vector2Int currentPos = queue.Dequeue();

                if (currentPos == buttonPos)
                {
                    break;
                }

                foreach (Vector2Int neighbor in GetNeighbors(currentPos))
                {
                    if (!parentMap.ContainsKey(neighbor) && grid[neighbor.x, neighbor.y] == GridType.Road)
                    {
                        parentMap[neighbor] = currentPos;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            if (parentMap.ContainsKey(buttonPos))
            {
                Vector2Int current = buttonPos;
                while (current != userPos)
                {
                    path.Add(current);
                    current = parentMap[current];
                }
                path.Reverse();
            }

            // ã�� ��θ� ���������� ǥ���մϴ�.
            foreach (Vector2Int pathPos in path)
            {
                mapTexture.SetPixel(pathPos.x, pathPos.y, Color.red);
            }
            isProcessImage = true;
            mapTexture.Apply();
        }

    }
    List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        if (pos.x > 0) neighbors.Add(new Vector2Int(pos.x - 1, pos.y));
        if (pos.x < gridSizeX - 1) neighbors.Add(new Vector2Int(pos.x + 1, pos.y));
        if (pos.y > 0) neighbors.Add(new Vector2Int(pos.x, pos.y - 1));
        if (pos.y < gridSizeY - 1) neighbors.Add(new Vector2Int(pos.x, pos.y + 1));
        return neighbors;
    }
    Vector2Int FindClosestRoadCoordinate(Vector2 position)
    {
        int closestX = Mathf.Clamp(Mathf.RoundToInt(position.x * gridSizeX), 0, gridSizeX - 1);
        int closestY = Mathf.Clamp(Mathf.RoundToInt(position.y * gridSizeY), 0, gridSizeY - 1);

        while (grid[closestX, closestY] != GridType.Road)
        {
            // ���� ����� �ε� ��ǥ�� �ƴ϶�� ������ ��ǥ�� �̵��Ͽ� �˻��մϴ�.
            closestX = Mathf.Clamp(closestX + 1, 0, gridSizeX - 1);
            closestY = Mathf.Clamp(closestY + 1, 0, gridSizeY - 1);
        }
        return new Vector2Int(closestX, closestY);
    }

    public RawImage displayRawImage;
    public void InitStartProcessImage(Vector2 userPosition, Vector2 buttonPosition)
    {
        // �̹��� ó�� �� ��� Ž��
        StartCoroutine("StartProcessImageNormalNavi");
    }

    IEnumerator StartProcessImageNormalNavi()
    {
        while (true)
        {
            ProcessImage(Manager.UI.userPosition, buttonsPos);

            // RawImage�� �� �̹��� ���
            Texture2D subsetMapImage = _naverMapAPI.GetMapImageSubset(userPos, 240, 200);
            displayRawImage.texture = subsetMapImage;

            yield return new WaitForSeconds(3f);
        }
    }
}