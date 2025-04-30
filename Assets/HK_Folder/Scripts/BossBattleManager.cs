using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossBattleManager : MonoBehaviour
{
    public enum BossState
    {
        StartSequence,
        ProtoManIntro,
        ProtoManFight,
        ProtoManDefeated,
        BassIntro,
        BassFight,
        BassDefeated,
        GameClear
    }

    public BossState currentState;

    public GameObject protoMan;
    public GameObject bass;
    public Transform protoManSpawnPoint;
    public Transform bassSpawnPoint;

    public Animator protoManAnimator;
    public Animator bassAnimator;

    public Text dialogueText;
    public GameObject errorCodeItem;

    void Start()
    {
        currentState = BossState.StartSequence;
        StartCoroutine(HandleState());
    }

    IEnumerator HandleState()
    {
        while (true)
        {
            switch (currentState)
            {
                case BossState.StartSequence:
                    yield return new WaitForSeconds(1f);
                    currentState = BossState.ProtoManIntro;
                    break;

                case BossState.ProtoManIntro:
                    yield return StartCoroutine(SpawnBoss(protoMan, protoManSpawnPoint, protoManAnimator, "IntroLanding"));
                    yield return StartCoroutine(ShowDialogue("...넌 누구지?"));
                    currentState = BossState.ProtoManFight;
                    break;

                case BossState.ProtoManFight:
                    yield return new WaitUntil(() => protoMan.GetComponent<BossHealth>().IsDead);
                    currentState = BossState.ProtoManDefeated;
                    break;

                case BossState.ProtoManDefeated:
                    yield return StartCoroutine(ShowDialogue("크윽... 넌 강하군... 하지만 다음은 다를 거야."));
                    yield return new WaitForSeconds(1f);
                    currentState = BossState.BassIntro;
                    break;

                case BossState.BassIntro:
                    yield return StartCoroutine(SpawnBoss(bass, bassSpawnPoint, bassAnimator, "IntroLanding"));
                    yield return StartCoroutine(ShowDialogue("내가 직접 상대하지. 각오해라!"));
                    currentState = BossState.BassFight;
                    break;

                case BossState.BassFight:
                    yield return new WaitUntil(() => bass.GetComponent<BossHealth>().IsDead);
                    currentState = BossState.BassDefeated;
                    break;

                case BossState.BassDefeated:
                    yield return StartCoroutine(ShowDialogue("말도 안 돼... 이런 녀석에게..."));
                    yield return new WaitForSeconds(1f);
                    Instantiate(errorCodeItem, bass.transform.position, Quaternion.identity);
                    currentState = BossState.GameClear;
                    break;

                case BossState.GameClear:
                    yield return StartCoroutine(ShowDialogue("ErrorCode.exe 획득!"));
                    // 게임 클리어 처리
                    yield break;
            }

            yield return null;
        }
    }

    IEnumerator SpawnBoss(GameObject boss, Transform spawnPoint, Animator anim, string introTrigger)
    {
        boss.transform.position = spawnPoint.position;
        boss.SetActive(true);
        anim.SetTrigger(introTrigger);
        yield return new WaitForSeconds(2f); // 착지 연출 시간
    }

    IEnumerator ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialogueText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        dialogueText.gameObject.SetActive(false);
    }
}


