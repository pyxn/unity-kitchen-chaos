// KitchenObject.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            // Debug.LogError("IKitchenObjectParent already has a KitchenObject!");
        }

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        if (kitchenObjectSO.prefab == null)
        {
            Debug.LogError("KitchenObject.cs: KitchenObjectSO Prefab does not exist.");
            return null;
        }

        Debug.Log("Spawning kitchen object: " + kitchenObjectSO.prefab.name);
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        if (kitchenObjectTransform == null)
        {
            Debug.LogError("Failed to instantiate prefab: " + kitchenObjectSO.prefab.name);
            return null;
        }

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        if (kitchenObject == null)
        {
            Debug.LogError("KitchenObject component not found on instantiated prefab: " + kitchenObjectSO.prefab.name);
            return null;
        }

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }



}
