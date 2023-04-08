using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Models.Constants;

namespace Assets.Scripts.GameObjects
{
    public class TileFill : MonoBehaviour
    {
        public TextMeshProUGUI ValueText;
        public Image Image;

        public int Level 
        {
            get => level;
            set
            {
                level = value;
                ValueText.text = Math.Pow(2, level).ToString();
                UpdateFillColor();
            } 
        }
        private int level;

        public float MoveTransitionSpeed;
        public float ScaleTransitionSpeed;
        private Vector3 targetScale;

        private void Awake()
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
            targetScale = new Vector3(1, 1, 1);
            Level = 1;

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

        private void UpdateFillColor()
        {
            var colorIndex = (Level - 1) % (TILE_COLORS.Length + 1);
            var colorHex = TILE_COLORS[colorIndex];

            if (colorIndex > 1) ValueText.color = Color.white;
            if (ColorUtility.TryParseHtmlString(colorHex, out Color color)) Image.color = color;
        }
    }
}
