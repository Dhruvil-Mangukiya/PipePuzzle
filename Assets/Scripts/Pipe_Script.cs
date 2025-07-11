using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipe_Script : MonoBehaviour
{
    public float[] rotations = { 0, 90, 180, 270 };

    public float[] correctRotation;

    [SerializeField] bool isPlaced = false;

    GameManager_script gameManager_Script;
    LevelTimer levelTimer;

    private float rotationTolerance = 0.1f;

    private void Awake()
    {
        gameManager_Script = GameObject.Find("GameManager").GetComponent<GameManager_script>();
        GameObject levelTimerObject = GameObject.Find("GameManager");

        if (levelTimerObject != null)
        {
            levelTimer = levelTimerObject.GetComponent<LevelTimer>();
            if (levelTimer == null)
            {
                Debug.LogError("LevelTimer script not found on the GameObject named 'LevelTimer'.");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'LevelTimer' not found in the scene.");
        }
    }

    public void randomPipe_rotation()
    {
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
    }

    private void Start()
    {
        randomPipe_rotation();

        if (correctRotation.Length > 1)
        {
            if (IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[0]) || IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[1]))
            {
                isPlaced = true;
                gameManager_Script.correctMove();
            }
        }
        else
        {
            if (IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[0]))
            {
                isPlaced = true;
                gameManager_Script.correctMove();
            }
        }
    }

    private void OnMouseOver()
    {
        if (GameManager_script.Instance.isGamePaused)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!levelTimer.IsTimerRunning())
            {
                levelTimer.StratTimer();
            }

            RotatePipe(90f);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (!levelTimer.IsTimerRunning())
            {
                levelTimer.StratTimer();
            }

            RotatePipe(-90f);
        }

    }

    private void RotatePipe(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle));
        Debug.Log($"[Pipe Rotation] New Rotation: {transform.eulerAngles.z} degrees");

        if (correctRotation.Length > 1)
        {
            if ((IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[0]) || IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[1])) && !isPlaced)
            {
                isPlaced = true;
                gameManager_Script.correctMove();
            }
            else if ((transform.eulerAngles.z != correctRotation[0] && transform.eulerAngles.z != correctRotation[1]) && isPlaced)
            {
                isPlaced = false;
                gameManager_Script.wrongMove();
            }
        }
        else
        {
            if (IsRotationCloseEnough(transform.eulerAngles.z, correctRotation[0]) && !isPlaced)
            {
                isPlaced = true;
                gameManager_Script.correctMove();
            }
            else if (transform.eulerAngles.z != correctRotation[0] && isPlaced)
            {
                isPlaced = false;
                gameManager_Script.wrongMove();
            }
        }
    }

    private bool IsRotationCloseEnough(float currentRotation, float targetRotation)
    {
        return Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) < rotationTolerance;
    }
}
