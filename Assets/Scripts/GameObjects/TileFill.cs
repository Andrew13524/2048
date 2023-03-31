using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class TileFill : MonoBehaviour
    {
        public float TransitionSpeed;
        private Vector3 targetScale;
        private void Awake()
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            targetScale = new Vector3(1, 1, 1);
            StartCoroutine(SpawnTransition());
        }

        private IEnumerator SpawnTransition()
        {
            while (Vector3.Distance(targetScale, gameObject.transform.localScale) > 0.01f)
            {
                gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, TransitionSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
