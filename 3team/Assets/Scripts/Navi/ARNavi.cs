using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavi : MonoBehaviour
{
    // 경로 표시 함수
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
        lineRenderer.gameObject.layer = LayerMask.NameToLayer("NaviCamera");
    }
}
