using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public Transform[] movableObjects;
    public float sceneMoveDuration = 0.5f;
    public float sceneStepY = 1.0f;

    public Transform gameScene;
    public Transform spawnMover;
    public Transform spawnPoint;
    public GameObject blockPrefab;
    public GameObject fakePrefab;

    public List<GameObject> blocks = new List<GameObject>();

    public bool canPress;
    private bool canMove;
    public bool gameStarted;

    public float speed = 2f;

    public float leftLimit = -5f;
    public float rightLimit = 5f;

    private int direction = 1;


    private List<Vector3> startMovableObjectPos = new();

    private void Start()
    {
        foreach (Transform t in movableObjects)
        {
            startMovableObjectPos.Add(t.position);
        }
    }
    public void CleanGame()
    {
        StopAllCoroutines();
        fakePrefab.SetActive(true);
        canPress = false;
        canMove = false;    
        gameStarted = false;

        for (int i = 0;i <  movableObjects.Length; i++)
        {
            movableObjects[i].transform.position = startMovableObjectPos[i];
        }


        foreach(var block in blocks)
        {
            Destroy(block.gameObject);
        }
        blocks.Clear();

        speed = 2;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        canPress = true;
        canMove = true;
        gameStarted = true;

    }

    private void Update()
    {
        if (!gameStarted) return;

        if(blocks.Count > 0)
            CheckLoseCondition(blocks[^1]);

        if (!canMove) return;

        MoveSpawnPoint();

        if (!canPress) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            bool isPressed = touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved;

            if(isPressed)
            {
                StartCoroutine(DropBlock());
            }
        }
        else if (Input.GetMouseButton(0))
        {
            StartCoroutine(DropBlock());
        }
    }
    private void CheckLoseCondition(GameObject block)
    {
        float blockY = block.transform.position.y;
        float cameraY = Camera.main.transform.position.y;

        if (blockY < cameraY -25)
        { 
            Lose();
        }
    }

    private void Lose()
    {
        Time.timeScale = 0;

        int newScore = PlayerPrefs.GetInt("Score");
        newScore += Mathf.RoundToInt(blocks.Count * speed);
        PlayerPrefs.SetInt("Score", newScore);

        WinGame winGame = (WinGame) UIManager.Instance.GetPopup(PopupTypes.WinGame);
        winGame.SetText(Mathf.RoundToInt(blocks.Count * speed));
        UIManager.Instance.ShowPopup(PopupTypes.WinGame);
        gameStarted = false;
    }
    private void MoveSpawnPoint()
    {

        spawnMover.transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        if (spawnMover.transform.position.x >= rightLimit)
        {
            direction = -1;
        }
        else if (spawnMover.transform.position.x <= leftLimit)
        {
            direction = 1;
        }
    }

    private IEnumerator DropBlock()
    {
        if (speed < 100)
        {
            speed += 2f;
        }
        canPress = false;
        fakePrefab.SetActive(false);
        GameObject newBlock = Instantiate(blockPrefab, gameScene);
        newBlock.transform.position = spawnPoint.transform.position;
        blocks.Add(newBlock);

        yield return new WaitForSeconds(2);
        canMove = false;
        StartCoroutine(MoveBlocksUp());
        fakePrefab.SetActive(true);
        canPress = true;
    }

    private IEnumerator MoveBlocksUp()
    {
        float elapsed = 0f;
        float duration = sceneMoveDuration;

        Vector3[] startPositions = new Vector3[movableObjects.Length];
        Vector3[] targetPositions = new Vector3[movableObjects.Length];

        for (int i = 0; i < movableObjects.Length; i++)
        {
            if (movableObjects[i] != null)
            {
                startPositions[i] = movableObjects[i].position;
                targetPositions[i] = startPositions[i] + new Vector3(0, sceneStepY, 0);
            }
        }

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            for (int i = 0; i < movableObjects.Length; i++)
            {
                if (movableObjects[i] != null)
                {
                    movableObjects[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < movableObjects.Length; i++)
        {
            if (movableObjects[i] != null)
            {
                movableObjects[i].position = targetPositions[i];
            }
        }

        canMove = true;
    }
}
