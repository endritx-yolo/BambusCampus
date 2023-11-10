using Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _OnTriggerEnter : MonoBehaviour
    {
        [SerializeField] private GameObject StaffPanel;
        [SerializeField] private GameObject chatPanel;

  

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StaffPanel.gameObject.SetActive(true);
                chatPanel.gameObject.SetActive(false);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StaffPanel.gameObject.SetActive(false);
                chatPanel.gameObject.SetActive(true);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
