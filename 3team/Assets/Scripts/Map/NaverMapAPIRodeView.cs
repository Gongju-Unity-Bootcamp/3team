using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NaverMapAPIRodeView : MonoBehaviour
{
    public RawImage mapRawImage;
    public Texture2D mapTexture { get; set; }
    public string strBaseURL = "";
    public string latitude = "";
    public string longitude = "";
    public string clientId = "";
    public string clientSecret = "";
    public string level = "18";
    public RectTransform mapRectTransform;

    private string mapWidth = "";
    private string mapHeight = "";
    private float brightnessThreshold = 0.964444444444444f;

    // 추가된 변수
    private bool[,] visited; // 방문한 좌표를 기록하기 위한 배열

    private void Start()
    {
        mapWidth = mapRectTransform.sizeDelta.x.ToString();
        mapHeight = mapRectTransform.sizeDelta.y.ToString();
        mapRawImage = GetComponent<RawImage>();
        StartCoroutine(MapLoader());
    }

    IEnumerator MapLoader()
    {
        string str = $"{strBaseURL}?center={longitude},{latitude}&level={level}&w={mapWidth}&h={mapHeight}";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientId);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            mapTexture = DownloadHandlerTexture.GetContent(request);
            mapRawImage.texture = mapTexture;
        }
        DrawRoad();
    }

    private void DrawRoad()
    {
        int gridSizeX = mapTexture.width;
        int gridSizeY = mapTexture.height;
        visited = new bool[gridSizeX, gridSizeY]; 

        if (IsRoad(0, 0) && !visited[0, 0])
        {
            BFS(0, 0, gridSizeX, gridSizeY);
        }


        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                if (visited[i, j])
                {
                    mapTexture.SetPixel(i, j, Color.blue);
                }
            }
        }

        mapTexture.Apply(); 
    }

    private bool IsRoad(int x, int y)
    {
        Color pixelColor = mapTexture.GetPixel(x, y);
        float brightness = (pixelColor.r + pixelColor.g + pixelColor.b) / 3f;
        return brightness >= brightnessThreshold;
    }

    private void BFS(int startX, int startY, int gridSizeX, int gridSizeY)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int i = 0; i < 4; i++)
            {
                int newX = current.x + dx[i];
                int newY = current.y + dy[i];

                if (newX >= 0 && newX < gridSizeX && newY >= 0 && newY < gridSizeY && !visited[newX, newY] && IsRoad(newX, newY))
                {
                    visited[newX, newY] = true;
                    queue.Enqueue(new Vector2Int(newX, newY));
                }
            }
        }
    }
}
