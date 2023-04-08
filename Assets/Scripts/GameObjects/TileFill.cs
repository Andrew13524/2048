using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class TileFill : MonoBehaviour
    {
        public TextMeshProUGUI ValueText;

        public int Value 
        { 
            get => value; 
            set
            {
                this.value = value;
                ValueText.text = this.value.ToString();
            }
        }
        private int value;

        public float MoveTransitionSpeed;
        public float ScaleTransitionSpeed;
        private Vector3 targetScale;
        private void Awake()
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            targetScale = new Vector3(1, 1, 1);
            Value = 2;

            StartCoroutine(SpawnTransition());
        }
        private void OnTransformParentChanged()
        {
            StartCoroutine(MoveTransition());
        }

        private IEnumerator SpawnTransition()
        {
            while (Vector3.Distance(targetScale, gameObject.transform.localScale) > 0.01f)
            {
                gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, ScaleTransitionSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        private IEnumerator MoveTransition()
        {
            while (Vector3.Distance(Vector3.zero, gameObject.transform.localPosition) > 0.01f)
            {
                gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, Vector3.zero, MoveTransitionSpeed * Time.deltaTime);
                yield return null;
            }

            var tileFills = gameObject.GetComponentInParent<Tile>().GetComponentsInChildren<TileFill>();
            if (tileFills.Length > 1)
            {
                if(this == gameObject.GetComponentInParent<Tile>().GetComponentsInChildren<TileFill>().Last())
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
