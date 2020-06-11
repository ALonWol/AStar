using System.Collections.Generic;
using UnityEngine;

public class GridGraph : IGraph {
    public INode[,] Data { get; private set; }

    public bool Passable(INode node) {
        return ((GridNode)node).Passable;
    }

    public bool SetPassable(int x, int y, bool passable) {
        var node = GetNode(x, y);
        if (node == null) {
            return false;
        }
        ((GridNode)node).Passable = passable;
        return true;
    }

    public int Cost(INode current, INode next) {
        int g = Heuristic(current, next);
        return (current.G + g);
    }

    public int Heuristic(INode current, INode goal) {
        var pos1 = ((GridNode)current).Position;
        var pos2 = ((GridNode)goal).Position;
        int dx = Mathf.Abs(pos1.x - pos2.x);
		int dy = Mathf.Abs(pos1.z - pos2.z);
		return (dx + dy);
    }

    public List<INode> Neighbors(INode node) {
        var pos = ((GridNode)node).Position;
        int x = pos.x / GridNode.Size;
        int y = pos.z / GridNode.Size;

        List<INode> lst = new List<INode>();
        if (y + 1 < Data.GetLength(1)) {
            lst.Add(GetNode(x, y + 1));
        }
        if (x + 1 < Data.GetLength(0)) {
            lst.Add(GetNode(x + 1, y));
        }
        if (y - 1 >= 0) {
            lst.Add(GetNode(x, y - 1));
        }
        if (x - 1 >= 0) {
            lst.Add(GetNode(x - 1, y));
        }

        return lst;
    }

    public INode GetNode(int x, int y) {
        if ((x >= 0 && x < Data.GetLength(0)) && (y >= 0 && y < Data.GetLength(1))) {
            return Data[x, y];
        }
        return null;
    }

    public void Build(Vector3Int[,] posLst) {
        int x = posLst.GetLength(0);
        int y = posLst.GetLength(1);

        Data = new INode[x, y];

        for (int i = 0; i < x; i++){
            for (int j = 0; j < y; j++) {
                bool passable = true;
                Data[i, j] = new GridNode(posLst[i, j], passable);
            }
        }
    }
}
