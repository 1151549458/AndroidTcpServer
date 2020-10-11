using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace SQFramework
{
    public class FishMove : FishBase
    {
        public GameObject Target;
        public float timer;
        public SkinnedMeshRenderer skMsRd;

        public bool isMove;
        private Coroutine coroutine;
        private Animator animator;
        // Use this for initialization
        IEnumerator Start()
        {
            transform.parent.position = MathUtil.GetRandomValue(rangeR, rangeE);
            isMove = true;
            animator = GetComponent<Animator>();
            animator.speed = 1;
            coroutine = null;
            fish_state = FISH_STATE.FISH_NONE;
            yield return new WaitForSeconds(3.0f);
            fish_state = FISH_STATE.FISH_NORMAL;

        }

        public void InitFish(float lv)
        {
            Level = lv;
            transform.localScale = new Vector3(Level, Level, Level);
            transform.parent.position = MathUtil.GetRandomValue(rangeR, rangeE);
            //Debug.Log(Level);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(name + "Level" + Level);
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Onscare());
            }
        }
        public override void OnPointerExit(PointerEventData eventData)
        {

        }

        private void OnCollisionEnter(Collision collision)
        {//��֮�����ײ
            if (collision.collider.tag == fishTag)
            {
                if (fish_state == FISH_STATE.FISH_NORMAL &&
                    collision.collider.gameObject.GetComponent<FishMove>().fish_state == FISH_STATE.FISH_NORMAL)
                {
                    //  Debug.Log(this.name + "Collision" + collision.gameObject.name);
                    if (Level < collision.collider.gameObject.GetComponent<FishMove>().Level)
                        RunAway();
                    else if (Level > collision.gameObject.GetComponent<FishMove>().Level)
                        collision.gameObject.GetComponent<FishMove>().RunAway();
                }
            }
        }
        void Update()
        {
            if (Target == null)
            {
                Target = new GameObject(gameObject.name + "TargetClone");
                //Target = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Target.transform.position = MathUtil.GetRandomValue(rangeR, rangeE);
            }
            if (!isMove)
                return;
            if (Vector3.Distance(this.transform.parent.position, Target.transform.position) < 0.2f) //��������
            {
                Target.transform.position = MathUtil.GetRandomValue(rangeR, rangeE); //�����������
            }
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, Target.transform.position, Time.deltaTime * speed);
            Vector3 lookDirPos = Target.transform.position - transform.parent.position;
            Quaternion lookDir = Quaternion.LookRotation(lookDirPos);
            transform.parent.localRotation = Quaternion.Lerp(transform.parent.rotation, lookDir, Time.deltaTime * rotaSpeed);
        }

        void Scare()
        {

        }
        public void RunAway()
        {
            Target.transform.position = MathUtil.GetRandomValue(rangeR, rangeE);
            StartCoroutine(Onscare());
        }
        IEnumerator Onscare()
        {
            fish_state = FISH_STATE.FISH_SCARE;
            Target.transform.position = MathUtil.GetRandomValue(rangeR, rangeE);
            animator.speed = animator.speed * 2.0f;
            speed = speed * 3.0f;
            yield return new WaitForSeconds(1.5f);
            speed = speed / 3.0f;
            animator.speed = animator.speed / 2.0f;
            coroutine = null;
            fish_state = FISH_STATE.FISH_NORMAL;
        }

        public void DieState(GameObject go)
        {
            isMove = false;
            animator.speed = animator.speed * 3.0f;
            speed = 0;
            transform.parent.SetParent(go.transform);
            fish_state = FISH_STATE.FISH_DIE;
        }

        void OnDrawGizmosSelected()
        {
            //Draw the camera path
            if (Target == null)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, 0.3f);
            Gizmos.DrawLine(this.transform.position, Target.transform.position);
            Gizmos.DrawWireSphere(Target.transform.position, 0.3f);
        }
        private void OnDestroy()
        {
            Destroy(Target.gameObject);
        }
    }
}