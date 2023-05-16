using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour
{
    [SerializeField] int levelNum;
    [SerializeField] Sprite openLock;
    [SerializeField] AudioClip openLockClip;
    public Image image;
    private GameStatus gameStatus;
    private Animator animator;
    private bool hasCheckedLevelCompleted;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        animator = FindObjectOfType<Animator>();
        gameStatus = FindObjectOfType<GameStatus>();

        if(gameStatus.OpenLocks[levelNum])
        {
            image.enabled = false;
        }
    }

    private void Update()
    {
        if (gameStatus.LevelsCompleted[levelNum]  && !gameStatus.OpenLocks[levelNum] && !hasCheckedLevelCompleted)
        {
            image.raycastTarget = false;
            hasCheckedLevelCompleted = true;

            animator.SetBool("HasToOpenLock", true);
            AudioSource.PlayClipAtPoint(openLockClip, FindObjectOfType<Camera>().transform.position);
            
            gameStatus.OpenLocks[levelNum] = true;
        }
    }
}
