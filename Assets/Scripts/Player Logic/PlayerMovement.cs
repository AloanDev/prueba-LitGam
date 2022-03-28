using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player_Logic
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables

        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;

        private Vector3 _velocity;

        #endregion

        #region Methods

        void Update() //Move player
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            _velocity.y += gravity * Time.deltaTime;
            controller.Move(_velocity * Time.deltaTime);
        }

        #endregion
    }
}