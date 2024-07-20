using UnityEngine;
public class CarMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve appearingCurve;
    [SerializeField] private AnimationCurve idleCurve;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform middlePoint;
    [SerializeField] private Transform bottomPoint;
    private Transform currentPoint;
    private float curveTimer = 0f;
    private float appearingDuration;
    private Vector3 deathPosition;
    private bool keyUpIsUnprocessed;
    private bool keyDownIsUnprocessed;
    private void Start()
    {
        appearingDuration = appearingCurve.keys[appearingCurve.length - 1].time;

        EventBus.GameStarted += SetDefault;
        EventBus.GameEnded += SetDeathPosition;
    }
    private void Update()
    {
        if (GameStateManager.Current != GameStates.Running)
            return;
        if (Input.GetKeyDown(KeyCode.A))
            keyUpIsUnprocessed = true;
        if (Input.GetKeyDown(KeyCode.D))
            keyDownIsUnprocessed = true;
    }
    private void FixedUpdate()
    {
        if (GameStateManager.Current == GameStates.Paused)
        {
            return;
        }
        if (GameStateManager.Current == GameStates.Appearing)
        {
            MoveSmootly(appearingCurve);
            if(curveTimer >= appearingDuration)
            {
                curveTimer = 0f;
                EventBus.CarAppeared?.Invoke();
            }
        }
        else if (GameStateManager.Current == GameStates.Idle)
        {
            MoveSmootly(idleCurve);
        }
        else if (GameStateManager.Current == GameStates.Running)
        {
            TryTurn();
        }
        else if (GameStateManager.Current == GameStates.Ended)
        {
            transform.position = deathPosition;
        }
    }
    private void MoveSmootly(AnimationCurve curveType)
    {
        transform.localPosition = new Vector3(curveType.Evaluate(curveTimer), middlePoint.localPosition.y, middlePoint.localPosition.z);
        curveTimer += Time.fixedDeltaTime;
    }
    private void SetDefault()
    {
        keyDownIsUnprocessed = false;
        keyUpIsUnprocessed = false;
        transform.position = middlePoint.position;
        currentPoint = middlePoint;
        curveTimer = 0f;
    }
    private void SetDeathPosition()
    {
        deathPosition = transform.position;
    }
    private void TryTurn()
    {
        if (!keyUpIsUnprocessed && !keyDownIsUnprocessed)
        {
            return;
        }
        if (keyUpIsUnprocessed && keyDownIsUnprocessed)
        {
            keyUpIsUnprocessed = false;
            keyDownIsUnprocessed = false;
            return;
        }
        if (keyUpIsUnprocessed)
        {
            if (currentPoint == bottomPoint)
                currentPoint = middlePoint;
            else
                currentPoint = topPoint;

            keyUpIsUnprocessed = false;
        }
        else if (keyDownIsUnprocessed)
        {
            if (currentPoint == topPoint)
                currentPoint = middlePoint;
            else
                currentPoint = bottomPoint;

            keyDownIsUnprocessed = false;
        }
        transform.position = currentPoint.position;
    }
}
