using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _PhotosTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] previewImage;
        [SerializeField] private GameObject[] panelImages;
        [SerializeField] private GameObject chatPanel;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                for (int i = 0; i < previewImage.Length; i++)
                {
                    previewImage[i].gameObject.SetActive(true);
                }
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                chatPanel.gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetMouseButton(0))
                {
                    for (int i = 0; i < panelImages.Length; i++)
                    {
                        panelImages[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < previewImage.Length; i++)
            {
                previewImage[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].gameObject.SetActive(false);
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            chatPanel.gameObject.SetActive(true);
        }

        public void ShowImage1()
        {
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].SetActive(false);
            }

            if (panelImages.Length > 0)
            {
                panelImages[0].SetActive(true);
            }

        }

        public void ShowImage2()
        {
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].SetActive(false);
            }

            if (panelImages.Length > 1)
            {
                panelImages[1].SetActive(true);
            }

        }

        public void ShowImage3()
        {
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].SetActive(false);
            }

            if (panelImages.Length > 2)
            {
                panelImages[2].SetActive(true);
            }

        }

        public void ShowImage4()
        {
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].SetActive(false);
            }

            if (panelImages.Length > 3)
            {
                panelImages[3].SetActive(true);
            }

        }

        public void ShowImage5()
        {
            for (int i = 0; i < panelImages.Length; i++)
            {
                panelImages[i].SetActive(false);
            }

            if (panelImages.Length > 4)
            {
                panelImages[4].SetActive(true);
            }
        }
    }
}
