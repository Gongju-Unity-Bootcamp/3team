using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavi : MonoBehaviour
{
    // AR ī�޶��� Transform ������Ʈ
    public Transform arCameraTransform;

    // ��� ǥ�� �Լ�
    private List<Vector2Int> _path;
    public void StartDrawLine(List<Vector2Int> path)
    {
        _path = path;
        StartCoroutine("DisplayPath");
    }


    IEnumerator DisplayPath()
    {
        // Line Renderer ������Ʈ�� �����ɴϴ�.
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer�� �ʱ�ȭ�մϴ�.
        lineRenderer.positionCount = _path.Count;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1f;
        Material material = new Material(Shader.Find("Sprites/Default"));
        material.color = Color.red; // ���������� ����
        lineRenderer.material = material;


        while(true)
        {
            Vector2Int firstPoint = _path[0];

            // AR ī�޶��� ��ġ�� �������� ��� ��ǥ�� ����մϴ�.
            Vector3 cameraPosition = arCameraTransform.position;
            Vector3 direction = new Vector3(firstPoint.x - cameraPosition.x, 0.25f, firstPoint.y - cameraPosition.z).normalized;
            Vector3 startPosition = cameraPosition + direction * 2f;

            // Line Renderer�� ��ġ�� �����մϴ�.
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
