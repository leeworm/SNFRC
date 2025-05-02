using UnityEngine;

public class DashCommandDetector
{
    private float lastLeftTapTime = -1f;
    private float lastRightTapTime = -1f;
    private float doubleTapThreshold = 0.25f; // 초 단위

    public bool CheckDashCommand(out int direction)
    {
        direction = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Time.time - lastLeftTapTime < doubleTapThreshold)
            {
                direction = -1;
                return true;
            }
            lastLeftTapTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Time.time - lastRightTapTime < doubleTapThreshold)
            {
                direction = 1;
                return true;
            }
            lastRightTapTime = Time.time;
        }

        return false;
    }
}
