using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ComputerScreen : MonoBehaviour
{
    public static ComputerScreen Instance;

    public GameObject screenUI; // panel ref
    public GameObject resultTextPrefab;
    public Transform content;
    public TMP_InputField inputField; // input ref

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        inputField.onEndEdit.AddListener(HandleCommandInput);
        inputField.ActivateInputField();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftControl) && screenUI.activeSelf) {
            Close();
        }
    }

    public void Open() {
        screenUI.SetActive(true);
        inputField.ActivateInputField();
        FindObjectOfType<PlayerMovement>().SetInteracting(true);
        FindObjectOfType<MouseLook>().SetInteracting(true);
    }

    public void Close() {
        inputField.DeactivateInputField();
        FindObjectOfType<PlayerMovement>().SetInteracting(false);
        FindObjectOfType<MouseLook>().SetInteracting(false);
    }

    private void HandleCommandInput(string command) {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(command)) {
            string result = "";

            switch(command) {
            case "hello":
                result = "Hello, dipshit";
                break;
            case "time":
                result = "It's " + System.DateTime.Now.ToString("hh:mm") + ", dipshit";
                break;
            case "help":
                result = "lmao, dipshit";
                break;
            case "clear":
                ClearHistory();
                inputField.text = "";
                inputField.ActivateInputField();
                return;
            default:
                result = "wtf do you want dipshit";
                break;
        }

            DisplayResult(command, result);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    private void DisplayResult(string command, string result) {
        GameObject commandText = Instantiate(resultTextPrefab, content);
        commandText.GetComponent<TMP_Text>().text = "> " + command;

        GameObject resultText = Instantiate(resultTextPrefab, content);
        resultText.GetComponent<TMP_Text>().text = result;

        Canvas.ForceUpdateCanvases();

        ScrollRect scrollRect = content.transform.parent.GetComponentInParent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 0;
    }

    private void ClearHistory() {
        foreach (Transform child in content) {
            Destroy(child.gameObject);
        }
    }

    private void OnDestroy() {
        inputField.onEndEdit.RemoveListener(HandleCommandInput);
    }
}
