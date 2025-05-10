using UnityEngine;
using System.Collections.Generic;

public class JH_PlayerCommandInput : MonoBehaviour
{
    private List<string> inputBuffer = new List<string>();
    private float inputTimeWindow = 0.8f;  // 입력 유지 시간
    private float lastInputTime;

    public GameObject projectilePrefab;
    public Transform firePoint;

    // 미리 정의된 커맨드 조합
    private readonly string[] combo1 = { "Left", "Right", "Q" };
    private readonly string[] combo2 = { "Right", "Left", "A" };
    private readonly string[] combo3 = { "Left", "Right", "S" };

    void Update()
    {
        DetectInput();
        CheckCommand();
        ClearBufferIfTimedOut();
    }

    void DetectInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            AddInput("Left");

        if (Input.GetKeyDown(KeyCode.RightArrow))
            AddInput("Right");

        if (Input.GetKeyDown(KeyCode.Q))
            AddInput("Q");

        if (Input.GetKeyDown(KeyCode.A))
            AddInput("A");

        if (Input.GetKeyDown(KeyCode.S))
            AddInput("S");
    }

    void AddInput(string key)
    {
        inputBuffer.Add(key);
        lastInputTime = Time.time;
    }

    void CheckCommand()
    {
        if (inputBuffer.Count < 3) return;

        // 가장 최근 3개만 비교
        var recentInputs = inputBuffer.GetRange(inputBuffer.Count - 3, 3).ToArray();

        if (MatchesCombo(recentInputs, combo1) ||
            MatchesCombo(recentInputs, combo2) ||
            MatchesCombo(recentInputs, combo3))
        {
            FireProjectile();
            inputBuffer.Clear();
        }
    }

    bool MatchesCombo(string[] input, string[] combo)
    {
        for (int i = 0; i < combo.Length; i++)
        {
            if (input[i] != combo[i]) return false;
        }
        return true;
    }

    void ClearBufferIfTimedOut()
    {
        if (Time.time - lastInputTime > inputTimeWindow)
        {
            inputBuffer.Clear();
        }
    }

    void FireProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("발사!");
    }
}
