using UnityEngine;

// 点击地图格子设置或反设置为障碍物
public class GridTypeSetter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var grid = PickGrid();
            if (grid) {
                SetGridType(grid);
            }
        }
    }

    GameObject PickGrid() {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject grid = null;

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            grid = hit.collider.gameObject;
        }

        return grid;
    }

    void SetGridType(GameObject grid) {
        var pos = grid.transform.position;
        int x = (int)pos.x / GridNode.Size;
        int y = (int)pos.z / GridNode.Size;

        if (Map.Instance.IsGoal(x, y) || Map.Instance.IsStart(x, y)) {
            return;
        }

        if (Map.Instance.Passable(x, y)) {
            Map.Instance.SetPassable(x, y, false);
        } else {
            Map.Instance.SetPassable(x, y, true);
        }
    }
}