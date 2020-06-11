using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    int row = 10;

    [SerializeField]
    int column = 10;

    int oldRow;

    int oldColumn;

    bool updated = true;

    public int Row => row;

    public int Column => column;

    AStar astar = new AStar();

    GridGraph graph = new GridGraph();

    List<INode> path;

    GameObject[,] gridsObj;

    public static Map Instance;

    public GameObject startObj;

    public GameObject goalObj;

    private INode start;

    private INode goal;

    private void Awake() {
        Instance = this;
        
        Setup();
    }

    void Update()
    {
        // map is invalide!!
        if (Row <= 0 || Column <= 0) {
            Clear();
            return;
        }

        // update data if map row|column changed!
        if (updated && (oldRow != row || oldColumn != column)) {
            updated = false;
        }
        if (!updated) {
            Clear();
            Setup();
            updated = true;
            return; // skip this frame!
        }

        // 重置可通过的地图格为白色
        for (int i = 0; i < Row; i++) {
            for (int j = 0; j < Column; j++) {
                if (graph.Passable(graph.GetNode(i, j))) {
                    var go = gridsObj[i, j];
                    go.GetComponent<MeshRenderer>().material.color = Color.white;
                }
            }
        }

        // find path and draw it
        path = astar.Search(graph, start, goal);
        if (path != null && path.Count > 0) {
            Debug.Log("num explored nodes:" + astar.GetExploredCount());
            for (int i = 0; i < path.Count; i++) {
                var pos = ((GridNode)path[i]).Position;
                var go = gridsObj[pos.x, pos.z];
                go.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }

    void Setup() {
        oldRow = row;
        oldColumn = column;

        var posLst = new Vector3Int[Row, Column];
        gridsObj = new GameObject[Row, Column];
        for (int i = 0; i < Row; i++) {
            for (int j = 0; j < Column; j++) {
                posLst[i, j] = new Vector3Int(i * GridNode.Size, 0, j * GridNode.Size);

                var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.SetParent(transform);
                go.transform.localPosition = posLst[i, j];
                gridsObj[i, j] = go;
            }
        }
        graph.Build(posLst);

        start = graph.GetNode(0, 0);
        goal = graph.GetNode(Row - 1, Column - 1);
        UpdateObjPos();
    }

    void Clear() {
        path = null;

        if (gridsObj != null) {
            for (int i = 0, n = gridsObj.GetLength(0); i < n; i++) {
                for (int j = 0, m = gridsObj.GetLength(1); j < m; j++) {
                    Object.DestroyImmediate(gridsObj[i, j]);
                }
            }
            gridsObj = null;
        }
    }

    void UpdateObjPos() {
        startObj.transform.localPosition = ((GridNode)start).Position;
        goalObj.transform.localPosition = ((GridNode)goal).Position;
    }

    public void SetStart(int x, int y) {
        var result = graph.GetNode(x, y);
        if (result != null && graph.Passable(result)) {
            start = result != null ? result : start;
            start.Previous = null;
        } else {
            start = null;
        }
    }

    public void SetGoal(int x, int y) {
        var result = graph.GetNode(x, y);
        goal = result != null ? result : goal;
        goal.Previous = null;
    }

    public void SetPassable(int x, int y, bool passable) {
        if (graph.GetNode(x, y) == null) {
            return;
        }

        graph.SetPassable(x, y, passable);
        if (passable) {
            SetGridColor(x, y, Color.white);
        } else {
            SetGridColor(x, y, Color.black);
        }
    }

    private void SetGridColor(int x, int y, Color color) {
        gridsObj[x, y].GetComponent<MeshRenderer>().material.color = color;
    }

    public bool Passable(int x, int y) {
        return graph.Passable(graph.GetNode(x, y));
    }

    public bool IsStart(int x, int y) {
        if (start == null) {
            return false;
        }
        return ((GridNode)graph.GetNode(x, y)).Position == ((GridNode)start).Position;
    }

    public bool IsGoal(int x, int y) {
        return ((GridNode)graph.GetNode(x, y)).Position == ((GridNode)goal).Position;
    }
}
