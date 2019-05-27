using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;            
        public float smoothing = 5f;        
        Vector3 offset;

        void Start()
        {
            offset = transform.position - player.position;
        }

        void FixedUpdate()
        {
            if (player.position != null)
            {
                Vector3 targetCamPos = player.position + offset;
                // targetCamPos.y += 165;
                // targetCamPos.z -= 150;
                transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
            }
        }

        public void setTarget(Transform newTarget)
        {
            player = newTarget;
        }
    }
}