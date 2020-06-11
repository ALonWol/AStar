using UnityEngine;

public class SetStart : Dragable {
    protected override void OnDrag() {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        Map.Instance.SetStart(x, y);
    }
}
