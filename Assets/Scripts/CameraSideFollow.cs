﻿using UnityEngine;
using System.Collections;

namespace Rolficopter.LD34.Assets.Scripts
{
    public class CameraSideFollow : MonoBehaviour
    {

        public Transform target;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, target.transform.position.x + 6, Time.deltaTime),
                transform.position.y,
                transform.position.z);
        }
    }
}