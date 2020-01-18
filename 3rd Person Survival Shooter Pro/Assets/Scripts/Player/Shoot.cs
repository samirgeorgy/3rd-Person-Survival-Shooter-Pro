using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    #region Private Variables

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootWeapon();
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Shoots the weapon
    /// </summary>
    private void ShootWeapon()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log("We hit" + hitInfo.collider.gameObject.name);
                Health health = hitInfo.collider.GetComponent<Health>();

                if (health != null)
                    health.Damage(25);
            }
        }
    }

    #endregion
}
