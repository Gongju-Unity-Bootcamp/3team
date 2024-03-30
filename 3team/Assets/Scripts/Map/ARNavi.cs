using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavi : MonoBehaviour
{
    // AR 카메라의 Transform 컴포넌트
    public Transform arCameraTransform;

    // 경로 표시 함수
    private List<Vector2Int> _path;
    public void StartDrawLine(List<Vector2Int> path)
    {
        _path = path;
        StartCoroutine("DisplayPath");
    }


    IEnumerator DisplayPath()
    {
        // Line Renderer 컴포넌트를 가져옵니다.
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer를 초기화합니다.
        lineRenderer.positionCount = _path.Count;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        Material material = new Material(Shader.Find("Sprites/Default"));
        material.color = Color.red; // 빨간색으로 설정
        lineRenderer.material = material;


        while(true)
        {
            Vector2Int firstPoint = _path[0];

            // AR 카메라의 위치를 기준으로 경로 좌표를 계산합니다.
            Vector3 cameraPosition = arCameraTransform.position;
            Vector3 direction = new Vector3(firstPoint.x - cameraPosition.x, 0.25f, firstPoint.y - cameraPosition.z).normalized;
            Vector3 startPosition = cameraPosition + direction * 2f;

            // Line Renderer의 위치를 설정합니다.
            lineRenderer.positionCount = _path.Count;
            for (int i = 0; i < _path.Count; i++)
            {
                Vector3 position = new Vector3(_path[i].x, 0.25f, _path[i].y);
                lineRenderer.SetPosition(i, startPosition + position);
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
