using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum UnitType { NONE = 0, ALLY = 1, ENEMY = 2 }

public class BaseUnit : MonoBehaviour, IUnit, IDamageable
{
    [Header("Unit Configuration")]
    [SerializeField] private UnitType unitType;

    [Space]

    [SerializeField] private GameObject selectedVisual;
    [SerializeField] private GameObject testTarget;

    // Private
    [BoxGroup("Debug"), SerializeField, ReadOnly] private bool isSelected = false;
    [BoxGroup("Debug"), SerializeField, ReadOnly] private float currentHealth;
    [BoxGroup("Debug"), SerializeField, ReadOnly] private float maxHealth;

    // Components
    private UnitController controller;
    private TurretRotator turretRotator;

    // Actions
    public System.Action<float> OnHealthChanged;
    public System.Action<BaseUnit> OnUnitKilled;

    // Properties
    public float CurrentHealth => currentHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    private void Awake()
    {
        controller = GetComponent<UnitController>();
        turretRotator = GetComponentInChildren<TurretRotator>();
    }

    private void Start()
    {
        isSelected = false;
        selectedVisual.SetActive(false);
    }

    private void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                (Vector3, bool) worldPosition = RaycastSelector.instance.GetWorldPosition();
                if (worldPosition.Item2 == false) return;

                controller.ChangeDestination(worldPosition.Item1);
                OnDeselected();

                Debug.Log(worldPosition.Item1);
            }
        }

        turretRotator.RotateTurretTowards(testTarget.transform);
    }

    public void OnSelected()
    {
        if (isSelected)
        {
            OnDeselected();
            return;
        }

        isSelected = true;

        if (selectedVisual != null)
            selectedVisual.SetActive(true);

        UnitManager.instance.SelectUnit(this);

        Debug.Log("Unit was selected.");
    }

    public void OnDeselected()
    {
        isSelected = false;

        if (selectedVisual != null)
            selectedVisual.SetActive(false);

        UnitManager.instance.DeselectUnit(this);

        Debug.Log("Unit was deselected.");
    }

    public void MoveToPoint()
    {

    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        // TODO: Blow the tank up. 

        OnDeselected();
        OnUnitKilled?.Invoke(this);
    }
}
