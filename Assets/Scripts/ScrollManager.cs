using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f;

    void Update() {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.PageUp) || Input.GetAxis("Mouse ScrollWheel") > 0) {
            ScrollUp();
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.PageDown) || Input.GetAxis("Mouse ScrollWheel") < 0) {
            ScrollDown();
        }
    }

    private void ScrollUp() {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition + scrollSpeed * Time.deltaTime);
    }

    private void ScrollDown() {
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition - scrollSpeed * Time.deltaTime);
    }
}
