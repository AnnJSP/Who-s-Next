using UnityEngine;


[RequireComponent(typeof(Animator))]
public class HW_5_IK_Script : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    protected Animator animator;
    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;
    private RaycastHit hit;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //Вызывается при расчёте IK
    void OnAnimatorIK()
    {
        var startPosition = transform.position;
        var directionFollow = lookObj.position - startPosition;

        if (animator)
        {
            //Если, мы включили IK, устанавливаем позицию и вращение
            if (ikActive)
            {
                // Устанавливаем цель взгляда для головы
                if (lookObj != null)
                {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }
                // Устанавливаем цель для правой руки и выставляем её в позицию
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                //home work for left hand
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }



                if (!Physics.Raycast(startPosition, directionFollow, out hit, 2f, _mask))
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }
            // Если IK неактивен, ставим позицию и вращение рук и головы в изначальное положение
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
