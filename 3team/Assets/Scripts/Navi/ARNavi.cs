using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARNavi : MonoBehaviour
{
    // ��� ǥ�� �Լ�
    public void DisplayPath(List<Vector2Int> path)
    {
        // Line Renderer ������Ʈ�� �����ɴϴ�.
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

        // Line Renderer�� �ʱ�ȭ�մϴ�.
        lineRenderer.positionCount = path.Count;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // ����� �� ������ Line Renderer�� �߰��մϴ�.
        for (int i = 0; i < path.Count; i++)
        {
            Vector3 position = new Vector3(path[i].x, 0.1f, path[i].y); // ����� y���� �������� �ð������� ���̵��� �մϴ�.
            lineRenderer.SetPosition(i, position);
        }
        lineRenderer.gameObject.layer = LayerMask.NameToLayer("NaviCamera");
    }
}
