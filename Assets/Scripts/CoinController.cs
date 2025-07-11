using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

[Serializable]
public class ConsumableItem
{
    public string Name;
    public string Id;
    public string desc;
    public string price;
}

public class CoinController : MonoBehaviour, IDetailedStoreListener
{
    IStoreController m_StoreController;

    public ConsumableItem cItem;

    [SerializeField] TMP_Text coinText;

    void Start()
    {
        int currentValue = PlayerPrefs.GetInt("totalCoins");
        coinText.text = currentValue.ToString();
        SetUpBuilder();
    }

    void SetUpBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(cItem.Id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Success");
        m_StoreController = controller;
    }

    public void CoinButton()
    {
        // AddCoin(500);
        m_StoreController.InitiatePurchase(cItem.Id);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        print("Purchase Complete" + product.definition.id);

        if (product.definition.id == cItem.Id)
        {
            AddCoin(500);
        }
        return PurchaseProcessingResult.Complete;
    }

    public void AddCoin(int coin_Amount)
    {
        int currentValue = PlayerPrefs.GetInt("totalCoins");
        // if (int.TryParse(coinText.text, out currentValue))
        // {
        currentValue += coin_Amount;
        PlayerPrefs.SetInt("totalCoins", currentValue);
        coinText.text = currentValue.ToString();
        // }
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Initialize Failed" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("Initialize Failed" + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed");
    }
    
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        print("Purchase Failed");
    }
}
