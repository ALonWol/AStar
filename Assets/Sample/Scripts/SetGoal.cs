using UnityEngine;

public class SetGoal : Dragable {
    protected override void OnDrag() {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        Map.Instance.SetGoal(x, y);
    }
}
