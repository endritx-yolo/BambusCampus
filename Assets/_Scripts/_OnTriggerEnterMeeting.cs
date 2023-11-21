using Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _OnTriggerEnterMeeting : MonoBehaviour
    {
        [SerializeField] private GameObject donikaInfo;



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                donikaInfo.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                donikaInfo.gameObject.SetActive(false);
            }
        }
    }
}
