using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Actor), typeof(PlayerInventory))]
public class Player : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;
    private PlayerInventory inventory;

    private void Awake()
    {
        controls = new Controls();
        inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        // Add a sample health potion to the player's inventory for testing
        inventory.AddItem(new HealthPotion());
    }

    private void OnEnable()
    {
        controls.Player.SetCallbacks(this);
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Player.SetCallbacks(null);
        controls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        // Handle movement input
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        // Handle item selection input
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Get the selected item from the inventory
            Consumable selectedItem = inventory.Selected;

            if (selectedItem != null)
            {
                // Check the type of the selected item and perform the appropriate action
                if (selectedItem is HealthPotion)
                {
                    // Heal the player
                    HealPlayer((HealthPotion)selectedItem);
                }
                else if (selectedItem is Fireball)
                {
                    // Use fireball to deal damage to nearby enemies
                    UseFireball((Fireball)selectedItem);
                }
                else if (selectedItem is ScrollOfConfusion)
                {
                    // Use scroll of confusion to confuse nearby enemies
                    UseScrollOfConfusion((ScrollOfConfusion)selectedItem);
                }

                // Remove the used item from the inventory
                inventory.RemoveItem(selectedItem);
            }
        }
    }

    private void HealPlayer(HealthPotion potion)
    {
        // Heal the player using the Heal method of the Actor component
        GetComponent<Actor>().Heal(potion.HealAmount);
        UIManager.Get.AddMessage("You were healed by a health potion.", Color.green);
    }

    private void UseFireball(Fireball fireball)
    {
        // Get nearby enemies using GameManager's GetNearbyEnemies function
        var nearbyEnemies = GameManager.Instance.GetNearbyEnemies(transform.position);

        // Deal damage to all nearby enemies
        foreach (var enemy in nearbyEnemies)
        {
            enemy.DoDamage(fireball.Damage);
        }

        UIManager.Get.AddMessage($"You used a fireball and dealt {fireball.Damage} damage to nearby enemies!", Color.red);
    }

    private void UseScrollOfConfusion(ScrollOfConfusion scroll)
    {
        // Get nearby enemies using GameManager's GetNearbyEnemies function
        var nearbyEnemies = GameManager.Instance.GetNearbyEnemies(transform.position);

        // Confuse all nearby enemies
        foreach (var enemy in nearbyEnemies)
        {
            enemy.GetComponent<Enemy>().Confuse();
        }

        UIManager.Get.AddMessage($"You used a scroll of confusion and confused nearby enemies!", Color.blue);
    }
}
