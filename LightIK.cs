using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace lik
{

    [ExecuteAlways]

    public class LightIK : MonoBehaviour
    {

        enum UpdateMode
        {
            Update,
            LateUpdate,
            FixedUpdate,
            ManualUpdate
        }

        [SerializeField] UpdateMode m_updateMode;
        public Transform upperArm;
        public Transform forearm;
        public Transform hand;
        public Transform elbow;
        public Transform target;
        [Space(20)]
        public Vector3 uppperArm_OffsetRotation;
        public Vector3 forearm_OffsetRotation;
        public Vector3 hand_OffsetRotation;
        [Space(20)]
        public bool handMatchesTargetRotation = true;
        [Space(20)]
        public bool debug;

        float angle;
        float upperArm_Length;
        float forearm_Length;
        float arm_Length;
        float targetDistance;
        float adyacent;

        [SerializeField, Tooltip("Editor will solve IK in LateUpdate if this is checked")] bool m_autoUpdateInEditMode;

        void LateUpdate()
        {
            if (Application.isPlaying && m_updateMode == UpdateMode.LateUpdate)
            {
                OnUpdate();
            }

            if (m_autoUpdateInEditMode && Application.isEditor && !Application.isPlaying)
            {

                OnUpdate();
            }
        }


        void Update()
        {
            if (Application.isPlaying && m_updateMode == UpdateMode.Update)
            {
                OnUpdate();
            }
        }
        void FixedUpdate()
        {
            if (Application.isPlaying && m_updateMode == UpdateMode.FixedUpdate)
            {
                OnUpdate();
            }
        }

        // Update is called once per frame
        public void OnUpdate()
        {
            if (upperArm != null && forearm != null && hand != null && elbow != null && target != null)
            {
                upperArm.LookAt(target, elbow.position - upperArm.position);
                upperArm.Rotate(uppperArm_OffsetRotation);

                Vector3 cross = Vector3.Cross(elbow.position - upperArm.position, forearm.position - upperArm.position);



                upperArm_Length = Vector3.Distance(upperArm.position, forearm.position);
                forearm_Length = Vector3.Distance(forearm.position, hand.position);
                arm_Length = upperArm_Length + forearm_Length;
                targetDistance = Vector3.Distance(upperArm.position, target.position);
                targetDistance = Mathf.Min(targetDistance, arm_Length - arm_Length * 0.001f);

                adyacent = ((upperArm_Length * upperArm_Length) - (forearm_Length * forearm_Length) + (targetDistance * targetDistance)) / (2 * targetDistance);

                angle = Mathf.Acos(adyacent / upperArm_Length) * Mathf.Rad2Deg;

                upperArm.RotateAround(upperArm.position, cross, -angle);

                forearm.LookAt(target, cross);
                forearm.Rotate(forearm_OffsetRotation);

                if (handMatchesTargetRotation)
                {
                    hand.rotation = target.rotation;
                    hand.Rotate(hand_OffsetRotation);
                }

                if (debug)
                {
                    if (forearm != null && elbow != null)
                    {
                        Debug.DrawLine(forearm.position, elbow.position, Color.blue);
                    }

                    if (upperArm != null && target != null)
                    {
                        Debug.DrawLine(upperArm.position, target.position, Color.red);
                    }
                }

            }

        }

        void OnDrawGizmos()
        {
            if (debug)
            {
                if (upperArm != null && elbow != null && hand != null && target != null && elbow != null)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawLine(upperArm.position, forearm.position);
                    Gizmos.DrawLine(forearm.position, hand.position);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(upperArm.position, target.position);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(forearm.position, elbow.position);
                }
            }
        }


    }
}