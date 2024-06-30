// CuttingCounter.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                // move the kitchen object to the clear counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player is not holding any object

            }
        }
        else
        {
            // THere is a kitchen object here
            if (player.HasKitchenObject())
            {
                // Player is carrying something

            }
            else
            {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            // Cut it
            // Destroy whatever is on there
            // Instantiate the cut version

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);

        }
    }
}
