using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _OnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private GameObject StaffPanel;
        [SerializeField] private GameObject PressPKey;
        [SerializeField] private GameObject PressOKey;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PressPKey.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PressPKey.gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKey(KeyCode.P))
                {
                    PressPKey.gameObject.SetActive(false);
                    PressOKey.gameObject.SetActive(true);
                    StaffPanel.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                if (Input.GetKey(KeyCode.O))
                {
                    PressPKey.gameObject.SetActive(true);
                    PressOKey.gameObject.SetActive(false);
                    StaffPanel.gameObject.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }

            }
        }
    }
}
