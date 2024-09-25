using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    private void OnMouseDown() {
        ComputerScreen.Instance.Open();
    }
}
