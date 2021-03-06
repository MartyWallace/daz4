﻿using UnityEngine;
using UnityEngine.UI;
using DAZ4.Weapons;
using DAZ4.Pickups;
using DAZ4.Data;

namespace DAZ4.Creatures
{
    public class Player : Creature
    {
        [SerializeField]
        private GameObject weaponGameObject;

        public Inventory Inventory
        {
            get;
            private set;
        }

        public Text HealthTextBox
        {
            get
            {
                GameObject health = GameObject.Find("HealthTextBox");

                if (health)
                {
                    return health.GetComponent<Text>();
                }

                return null;
            }
        }

        protected override void Start()
        {
            base.Start();

            Inventory = GetComponent<Inventory>();

            if (HealthTextBox)
            {
                HealthTextBox.text = Stats.Health.ToString();
            }
        }

        protected override void Update()
        {
            base.Update();

            FacePoint(Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position));

            int horizontalInput = 0;
            int verticalInput = 0;

            if (Input.GetKey(KeyCode.W))
            {
                verticalInput = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalInput = -1;
            }

            if (Input.GetKey(KeyCode.A))
            {
                horizontalInput = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalInput = 1;
            }

            if (verticalInput != 0 || horizontalInput != 0)
            {
                float inputAngle = Mathf.Atan2(verticalInput, horizontalInput);

                Vector2 force = new Vector2(
                    Mathf.Cos(inputAngle) * Stats.Speed,
                    Mathf.Sin(inputAngle) * Stats.Speed
                );

                Body.AddForce(force);
            }

            if (Input.GetMouseButton(0)) {
                if (weaponGameObject) {
                    Weapon weapon = weaponGameObject.GetComponent<Weapon>();
                    weapon.Attack();
                }
            }
        }

        public override void TakeDamage(Damage damage)
        {
            base.TakeDamage(damage);

            if (HealthTextBox)
            {
                HealthTextBox.text = Stats.Health.ToString();
            }
        }

        protected override void Die()
        {
            base.Die();

            Debug.Log("Player has died");
        }
    }
}