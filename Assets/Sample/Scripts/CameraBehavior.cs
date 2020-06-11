using UnityEngine;

// To make sure map grids all in camera viewport!
public class CameraBehavior : MonoBehaviour
{
    readonly float unitSize = 0.5f / 1; // 相机视图大小和世界坐标大小为0.5:1

    Camera main;

    private void Awake() {
        main = Camera.main;
    }

    private void Start() {
        UpdateViewport();
    }

    // 根据地图宽高（相对较大的那个）确定相机视图大小和位置。
    public void UpdateViewport() {
        // 视图大小
        int row = Map.Instance.Row;
        int column = Map.Instance.Column;
        float screenRatio = Screen.width * 1f / Screen.height;
        float mapRatio = row * 1f / column;
        if (mapRatio < screenRatio) {
            // fit height
            main.orthographicSize = unitSize * column;
        } else {
            // fit width
            main.orthographicSize = (unitSize * row) * ((float)Screen.height / Screen.width);
        }

        // 位置
        float x = row * GridNode.Size / 2f - GridNode.Size / 2f;
        float y = main.transform.position.y;
        float z = column * GridNode.Size / 2f - GridNode.Size / 2f;
        transform.position = Map.Instance.transform.position + new Vector3(x, y, z);
    }
}
