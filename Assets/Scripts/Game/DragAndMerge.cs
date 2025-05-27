using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DragAndMerge : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    private MergeChicken myChicken;
    private bool isDragging = false;

    private void Awake()
    {
        cam = Camera.main;
        myChicken = GetComponent<MergeChicken>();
    }

    private void OnMouseDown()
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 0f;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition) + offset;
        newPos.z = 0;
        transform.position = newPos;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // ����������, �� ��'��� ������� �� �����
        if (!IsVisibleToCamera())
        {
            transform.position = Vector3.zero; // ���������� � ����� �����
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var col in colliders)
        {
            if (col.gameObject == gameObject) continue;

            MergeChicken other = col.GetComponent<MergeChicken>();
            if (other != null && other.Level == myChicken.Level)
            {
                MergeManager.Instance.TryMerge(myChicken, other);
                return;
            }
        }
    }

    // ��������, �� ��'��� ������� �� ����-��� �����
    private bool IsVisibleToCamera()
    {
        // �������� ������� ��'���� �� �����
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // ���� ��'��� ����������� �� ������ [0,1] �� X ��� Y - �� �� �������
        return (viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
                viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
                viewportPosition.z > 0); // Z > 0 ������, �� ��'��� ����� �������
    }
}
