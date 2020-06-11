using System.Collections.Generic;

public sealed class AStar {
    PriorityQueue<INode> open = new PriorityQueue<INode>();

    HashSet<INode> close = new HashSet<INode>();

    public List<INode> Search(IGraph graph, INode start, INode goal) {
        if (graph == null || start == null || goal == null) {
            return null;
        }

        open.Clear();
        close.Clear();

        open.Enqueue(start, start.F);

        while (open.Count > 0) {
            var current = open.Dequeue();
            close.Add(current);

            if (current == goal) {
                return BuildPath(goal);
            }

            var neighors = graph.Neighbors(current);

            if (neighors == null && open.Count > 0) {
                continue;
            }

            foreach (var neighor in neighors) {
                if (!graph.Passable(neighor) || close.Contains(neighor)) {
                    continue;
                }

                var g = graph.Cost(current, neighor);
                var h = graph.Heuristic(neighor, goal);
                if (!open.Contains(neighor)) {
                    neighor.G = g;
                    neighor.H = h;
                    neighor.Previous = current;
                    open.Enqueue(neighor, neighor.F);
                } else if (g + h < neighor.F) {
                    neighor.G = g;
                    neighor.H = h;
                    neighor.Previous = current;
                    open.UpdatePriority(neighor, neighor.F);
                }
            }
        }

        return null;
    }

    List<INode> BuildPath(INode goal) {
        var path = new List<INode>();
        for (var current = goal; current != null; current = current.Previous) {
            path.Add(current);
        }
        path.Reverse();
        return path;
    }

    // debug
    public int GetExploredCount() {
        return close.Count;
    }
}