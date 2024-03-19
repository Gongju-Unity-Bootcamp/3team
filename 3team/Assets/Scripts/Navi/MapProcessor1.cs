using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
public class MapProcessor1 : MonoBehaviour
{
    private Texture2D mapTexture;
    private int gridSizeX;
    private int gridSizeY;
    private float brightnessThreshold = 0.964444444444444f; // 밝기 값을 기준으로 하얀색 판별에 사용될 임계값
    private NaverMapAPI _naverMapAPI;
    // 그리드 타입을 나타내는 열거형
    public enum GridType
    {
        Road,  // 밝은 색으로 인식되는 경우 길
        Obstacle, // 어두운 색으로 인식되는 경우 장애물
    }

    // 이미지를 2중 배열로 변환한 그리드
    private GridType[,] grid;

    private Vector2 buttonsPos;
    public void Init(Vector2 userPosition, Vector2 buttonPosition)
    {
        _naverMapAPI = gameObject.GetComponent<NaverMapAPI>();
        
        mapTexture = _naverMapAPI.mapTexture;
        InitializeGrid();
        TextureClassification();
        buttonPosition = _naverMapAPI.Clamping(buttonPosition.x,buttonPosition.y);
        buttonsPos = buttonPosition;
        userPosition = _naverMapAPI.Clamping(userPosition.x, userPosition.y);
        StartCoroutine("StartProcessImage");

    }


    // 그리드 초기화
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
            Debug.LogError("mapTexture가 할당되지 않았습니다.");
        }
    }
    // 이미지 처리 함수

    public List<Vector2Int> path { get;set; }
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
                    //mapTexture.SetPixel(x, y, Color.red);
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
        while(true)
        {
            ProcessImage(Manager.UI.userPosition, buttonsPos);
            yield return new WaitForSeconds(9f);
        }
    }
    void ProcessImage(Vector2 userPosition, Vector2 buttonPosition)
    {
        
        if (mapTexture != null && grid != null)
        {
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            Dictionary<Vector2Int, Vector2Int> parentMap = new Dictionary<Vector2Int, Vector2Int>(); // 백트래킹을 위한 부모를 추적하는 사전

            Vector2Int userPos = FindClosestRoadCoordinate(userPosition);
            Vector2Int buttonPos = FindClosestRoadCoordinate(buttonPosition);
            Debug.Log(userPos);
            Debug.Log(buttonPos);
            queue.Enqueue(userPos);
            parentMap[userPos] = userPos;

            path = new List<Vector2Int>();

            while (queue.Count > 0)
            {
                Vector2Int currentPos = queue.Dequeue();

                // 버튼 위치에 도달하면 루프를 종료합니다.
                if (currentPos == buttonPos)
                {
                    Debug.Log("버튼에 도달함");
                    break;
                }

                // 인접한 노드 탐색
                foreach (Vector2Int neighbor in GetNeighbors(currentPos))
                {
                    if (!parentMap.ContainsKey(neighbor) && grid[neighbor.x, neighbor.y] == GridType.Road)
                    {
                        parentMap[neighbor] = currentPos;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            // 버튼 위치에서 역추적하여 경로를 찾습니다.
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

            // 찾은 경로를 빨간색으로 표시합니다.
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
            // 가장 가까운 로드 좌표가 아니라면 인접한 좌표로 이동하여 검사합니다.
            closestX = Mathf.Clamp(closestX + 1, 0, gridSizeX - 1);
            closestY = Mathf.Clamp(closestY + 1, 0, gridSizeY - 1);
        }
        return new Vector2Int(closestX, closestY);
    }

    public void DisplayPath(List<Vector2Int> path)
    {
        // Line Renderer 컴포넌트를 가져옵니다.
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer를 초기화합니다.
        lineRenderer.positionCount = path.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // 경로의 각 지점을 Line Renderer에 추가합니다.
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position = new Vector3(path[i].x, 0.1f, path[i].y); // 경로의 y값을 고정시켜 시각적으로 보이도록 합니다.
            lineRenderer.SetPosition(i, position);
        }
    }
}