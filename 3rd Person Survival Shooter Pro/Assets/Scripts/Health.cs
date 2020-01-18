using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth;

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Supporting Functions

    /// <summary>
    /// Inflicts Damage
    /// </summary>
    /// <param name="damageAmount">The damage amount</param>
    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < _minHealth)
            Destroy(this.gameObject);
    }

    #endregion
}
