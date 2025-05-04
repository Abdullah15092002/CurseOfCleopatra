using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Pointer
{
    public static class UIInputBlocker
    {
        /// <summary>
        /// Pointer UI üzerinde mi ve verdiğin tag'e sahip mi kontrol eder.
        /// </summary>
        public static bool IsPointerOverUIWithTag()
        {
            string tag = "UIButton";
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag(tag))
                    return true;
            }

            return false;
        }
    }
    }
