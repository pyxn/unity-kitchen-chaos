// ClearCounter.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{



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
                // check if the player is holding a plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    bool succeeded = plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO());

                    if (succeeded)
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is carrying something other than a plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // Counter is holding a plate
                        bool succeeded = plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO());
                        if (succeeded)
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
