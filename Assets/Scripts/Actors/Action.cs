using UnityEngine;

public class Action : MonoBehaviour
{
    static public void MoveOrHit(Actor actor, Vector2 direction)
    {
        // Controleren of de richting waarin we bewegen bezet is
        Actor target = GameManager.Get.GetActorAtLocation(actor.transform.position + (Vector3)direction);

        if (target == null)
        {
            // Als het target null is, voer de Move functie uit
            Move(actor, direction);
        }
        else
        {
            // Als er een target is, voer de Hit functie uit
            Hit(actor, target);
        }
    }

    static public void Move(Actor actor, Vector2 direction)
    {
        // Verplaats de actor in de opgegeven richting
        actor.Move(direction);

        // Update het gezichtsveld van de actor
        actor.UpdateFieldOfView();
    }

    static public void Hit(Actor actor, Actor target)
    {
        // Bereken de damage
        int damage = Mathf.Max(0, actor.Power - target.Defense);

        // Verminder de hitpoints van het target als er schade is
        if (damage > 0)
        {
            target.TakeDamage(damage);
            // Voeg een bericht toe via UIManager
            string message = $"{actor.name} hits {target.name} for {damage} damage!";
            if (actor.GetComponent<Player>() != null)
            {
                UIManager.Instance.AddMessage(message, Color.white);
            }
            else
            {
                UIManager.Instance.AddMessage(message, Color.red);
            }
        }
        else
        {
            // Voeg een bericht toe via UIManager dat er geen schade is
            string message = $"{actor.name} attacks {target.name}, but does no damage.";
            if (actor.GetComponent<Player>() != null)
            {
                UIManager.Instance.AddMessage(message, Color.white);
            }
            else
            {
                UIManager.Instance.AddMessage(message, Color.red);
            }
        }
    }
}
