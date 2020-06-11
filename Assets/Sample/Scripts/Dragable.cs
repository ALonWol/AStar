using UnityEngine;

public abstract class Dragable : MonoBehaviour {
    private void OnMouseDrag()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) {
            return;
        }

        transform.position = hit.collider.transform.position;
        OnDrag();
    }

    protected abstract void OnDrag();
}
