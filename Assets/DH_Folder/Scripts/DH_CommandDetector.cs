using UnityEngine;

public class DH_CommandDetector
{
    private int lastDirection = 0;
    private float lastTapTime = -1f;
    private bool tappedOnce = false;
    private bool wasReleased = true;

    private float doubleTapThreshold = 0.3f;

    public enum DashType { None, Forward, Backward }

    public DashType CheckCommand(int facingDir, bool enabled = true)
    {
        if (!enabled)
        {
            Reset(); // 기존 입력 초기화
            return DashType.None;
        }
        
        int inputDir = (int)Input.GetAxisRaw("Horizontal");

        // 0이면 방향키가 떨어졌다고 판단
        if (inputDir == 0)
        {
            wasReleased = true;
            return DashType.None;
        }

        // 두 번째 입력 조건
        if (tappedOnce &&
            inputDir == lastDirection &&
            Time.time - lastTapTime <= doubleTapThreshold &&
            wasReleased)
        {
            tappedOnce = false;
            wasReleased = false;

            if (inputDir == facingDir)
                return DashType.Forward;
            else if (inputDir == -facingDir)
                return DashType.Backward;
            else
                return DashType.None;
        }

        // 첫 입력 처리 또는 방향 바뀐 경우
        if (!tappedOnce || inputDir != lastDirection)
        {
            tappedOnce = true;
            lastDirection = inputDir;
            lastTapTime = Time.time;
            wasReleased = false;
        }

        return DashType.None;
    }

    public void Reset()
    {
        tappedOnce = false;
        wasReleased = true;
        lastDirection = 0;
        lastTapTime = -1f;
    }
}
