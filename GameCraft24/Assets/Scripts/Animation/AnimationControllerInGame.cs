using System.Collections;
using UnityEngine;

public class AnimationControllerInGame : MonoBehaviour
{

    private static AnimationControllerInGame instance;
    public static AnimationControllerInGame Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<AnimationControllerInGame>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("AnimationControllerInGame");
                    instance = obj.AddComponent<AnimationControllerInGame>();
                }
            }
            return instance;
        }
    }

    public GameObject beforeJumpAnimationPrefab;
    public GameObject afterJumpAnimationPrefab;

    public IEnumerator PlayBeforeJumpAnimation(Vector3 playerPosition)
    {
        // Instantiate the animation at the player's position
        GameObject beforeJumpAnimation = Instantiate(beforeJumpAnimationPrefab, playerPosition, Quaternion.identity);

        // Get the Animator component and trigger the animation
        Animator animator = beforeJumpAnimation.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("beforeJump");
        }

        // Wait for the duration of the animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the animation GameObject
        Destroy(beforeJumpAnimation);

        yield break;
    }

    public IEnumerator PlayAfterJumpAnimation(Vector3 playerPosition)
    {
        // Instantiate the animation at the player's position
        GameObject afterJumpAnimation = Instantiate(afterJumpAnimationPrefab, playerPosition, Quaternion.identity);

        // Get the Animator component and trigger the animation
        Animator animator = afterJumpAnimation.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("afterJump");
        }

        // Wait for the duration of the animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the animation GameObject
        Destroy(afterJumpAnimation);

        yield break;
    }
}
