using UnityEngine;
using System.Collections;
using System;

namespace HumanStates
{
    public class Normal : State<Human>
    {
        private float m_speed;
        private float m_time;
        private bool m_isWait;
        private bool m_isLeft;

        public override void Enter(Human entity)
        {
            m_isWait = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
            m_speed = GameDatabase.Instance().GetTimeData(GameManager.instance.m_stageNumber).humanSpeed;
            SetDirection();
        }
        public override void Update(Human entity)
        {
            if (!m_isWait)
            {
                float direction = 1f;
                if (m_isLeft)
                {
                    direction = -1f;
                    entity.m_directionObject.transform.rotation = entity.m_plusObject.transform.rotation;
                }
                else
                {
                    entity.m_directionObject.transform.rotation = entity.m_minusObject.transform.rotation;
                }
                entity.humanAngleValue += Time.deltaTime * m_speed * direction;
            }
            m_time += Time.deltaTime;
            if(m_time > 2f)
            {
                entity.ChangeState(new Normal());
            }
            if(entity.isDamaged)
            {
                entity.ChangeState(new Emergency());
            }
        }
        public override void Exit(Human entity)
        {
        }
        void SetDirection()
        {
            m_isLeft = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
        }
    }
    public class Emergency : State<Human>
    {
        private float m_speed;
        private float m_safeTime;
        private float m_feeberTime;
        private bool m_isFeeber;

        public override void Enter(Human entity)
        {
            entity.SetAnimation("isEmer", true);
            m_speed = GameDatabase.Instance().GetTimeData(GameManager.instance.m_stageNumber).humanESpeed;
            m_safeTime = 0f;
            m_feeberTime = 0f;
            m_isFeeber = false;
        }
        public override void Update(Human entity)
        {
            float direction = 1f;
            if (!m_isFeeber)
            {
                if (entity.angleValue - 90f < 0)
                {
                    direction = -1f;
                    entity.m_directionObject.transform.rotation = entity.m_plusObject.transform.rotation;
                }
                else
                {
                    entity.m_directionObject.transform.rotation = entity.m_minusObject.transform.rotation;
                }
            }
            else
            {
                if (entity.angleValue - 90f < 0)
                {
                    direction = 1f;
                    entity.m_directionObject.transform.rotation = entity.m_minusObject.transform.rotation;
                }
                else
                {
                    entity.m_directionObject.transform.rotation = entity.m_plusObject.transform.rotation;
                }
            }
            /*
            if (entity.angleValue - 90f < 0 && entity.earthAngleValue < 0)
            {
                direction = -1f;
                entity.m_directionObject.transform.rotation = entity.m_plusObject.transform.rotation;
            }
            else if (entity.angleValue - 90f < 0 && entity.earthAngleValue >= 0)
            {
                direction = 1f; //왼쪽클릭(오른쪽으로회전 + 오른쪽으로 도망침)
                entity.m_directionObject.transform.rotation = entity.m_minusObject.transform.rotation;
            }
            else if (entity.angleValue - 90f >= 0 && entity.earthAngleValue < 0)
            {
                direction = -1f;
                entity.m_directionObject.transform.rotation = entity.m_plusObject.transform.rotation;
            }
            else if (entity.angleValue - 90f >= 0 && entity.earthAngleValue >= 0)
            {
                direction = 1f; //오른쪽클릭
                entity.m_directionObject.transform.rotation = entity.m_minusObject.transform.rotation;
            }
            */
            entity.humanAngleValue += Time.deltaTime * m_speed * direction;

            if(entity.isDamaged)
            {
                m_safeTime = 0;
            }
            else
            {
                m_safeTime += Time.deltaTime;
                if(m_safeTime > 2f)
                {
                    entity.ChangeState(new Normal());
                }
            }

            m_feeberTime += Time.deltaTime;
            if(m_feeberTime > 1f && !m_isFeeber)
            {
                m_feeberTime = 0;
                m_speed *= 1.2f;
                m_isFeeber = true;
            }
        }
        public override void Exit(Human entity)
        {
            entity.SetAnimation("isEmer", false);
        }
    }
}