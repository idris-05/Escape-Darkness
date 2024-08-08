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
    public GameObject disappear1AnimationPrefab;
    public GameObject disappear2AnimationPrefab;
    public GameObject appear1AnimationPrefab;
    public GameObject appear2AnimationPrefab;
    public GameObject teleportWithBulletAnimationPrefab;




    // diappeare 1 w 2 , 3ndhom controller a animation asmhom normal fih disappear . 
    // appear hya le contraire t3 disappear : mala dir controller jdid , t5dem b la meme animation (li hya disappear) bsh ydirlha speed = -1 .
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
        GameObject afterJumpAnimation = Instantiate(afterJumpAnimationPrefab, playerPosition + new Vector3(-0.04f, -0.117f, 0), Quaternion.identity);

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

    public IEnumerator PlayDisappearAnimation(Vector3 playerPosition)
    {
        // Instantiate the animation at the player's position
        GameObject disappear1Animation = Instantiate(disappear1AnimationPrefab, playerPosition, Quaternion.identity);
        GameObject disappear2Animation = Instantiate(disappear2AnimationPrefab, playerPosition, Quaternion.identity);

        // Get the Animator component and trigger the animation
        Animator animator1 = disappear1Animation.GetComponent<Animator>();
        Animator animator2 = disappear2Animation.GetComponent<Animator>();


        // Start the two animations
        if (animator1 != null) animator1.Play("disappear1");
        if (animator2 != null) animator2.Play("disappear2");

        // Or if the animations have different lengths and you want to wait for the longer one
        float animation1Length = animator1.GetCurrentAnimatorStateInfo(0).length;
        float animation2Length = animator2.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(Mathf.Max(animation1Length, animation2Length));

        // Destroy the animation GameObject
        Destroy(disappear1Animation);
        Destroy(disappear2Animation);

        yield break;
    }


    public IEnumerator PlayAppearAnimation(Vector3 playerPosition)
    {
        // Instantiate the animation at the player's position
        GameObject appear1Animation = Instantiate(appear1AnimationPrefab, playerPosition, Quaternion.identity);
        GameObject appear2Animation = Instantiate(appear2AnimationPrefab, playerPosition, Quaternion.identity);

        // Get the Animator component and trigger the animation
        Animator animator1 = appear1Animation.GetComponent<Animator>();
        Animator animator2 = appear2Animation.GetComponent<Animator>();


        // Start the two animations
        if (animator1 != null) animator1.Play("disappear1");
        if (animator2 != null) animator2.Play("disappear2");

        // Or if the animations have different lengths and you want to wait for the longer one
        float animation1Length = animator1.GetCurrentAnimatorStateInfo(0).length;
        float animation2Length = animator2.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(Mathf.Max(animation1Length, animation2Length));

        // Destroy the animation GameObject
        Destroy(appear1Animation);
        Destroy(appear2Animation);

        yield break;
    }

    public IEnumerator PlayteleportWithBulletAnimation(Vector3 playerPosition)
    {
        // Instantiate the animation at the player's position
        GameObject teleportWithBulletAnimation = Instantiate(teleportWithBulletAnimationPrefab, playerPosition, Quaternion.identity);

        // Get the Animator component and trigger the animation
        Animator animator = teleportWithBulletAnimation.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("teleportWithBullet");
        }

        // Wait for the duration of the animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the animation GameObject
        Destroy(teleportWithBulletAnimation);

        yield break;
    }


}