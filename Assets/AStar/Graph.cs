using System.Collections.Generic;

public interface IGraph {
    bool Passable(INode node);

    int Cost(INode current, INode next);

    int Heuristic(INode current, INode goal);

    List<INode> Neighbors(INode node);
}

public interface INode : IPriorityQueueNode {
    int G { get;set; }

    int H { get;set; }

    int F { get; }

    INode Previous { get; set; }
}