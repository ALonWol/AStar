using UnityEngine;

public class GridNode : INode {
    public static readonly int Size = 1;

    public int G { get; set; }

    public int H { get; set; }

    public int F {
        get {
            return G + H;
        }
    }

    public INode Previous { get; set; }

    public int QueueIndex { get; set; }

    public float Priority { get; set; }

    public Vector3Int Position { get; }

    public bool Passable { get; set; }

    public GridNode(Vector3Int pos, bool passable) {
        this.Position = pos;
        this.Passable = passable;
        G = 0;
        H = int.MaxValue;
    }

    public override string ToString() {
        return "GridNode`[" + Position + "]";
    }
}
