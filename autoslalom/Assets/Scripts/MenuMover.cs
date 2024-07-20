using UnityEngine;
using UnityEngine.UI;
public class MenuMover : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private RectTransform menuToMove;
    [SerializeField] private AnimationCurve curve;
    private bool isOpened = false;
    private float curveDuration;
    private float curveTimer;
    private void OnEnable()
    {
        ToggleButtons(false);
        curveTimer = 0f;
    }
    private void Start()
    {
        curveDuration = curve.keys[curve.length - 1].time;
    }
    private void Update()
    {
        if (isOpened)
        {
            curveTimer += Time.deltaTime;
        }
        else
        {
            curveTimer -= Time.deltaTime;
        }
        if (Mathf.Abs(curveTimer) >= curveDuration)
        {
            isOpened = !isOpened;

            enabled = false;
            ToggleButtons(true);
            return;
        }
        menuToMove.anchoredPosition = new Vector2(curve.Evaluate(curveTimer) * 100, menuToMove.anchoredPosition.y);
    }
    private void ToggleButtons(bool state)
    {
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].interactable = state;
    }
}