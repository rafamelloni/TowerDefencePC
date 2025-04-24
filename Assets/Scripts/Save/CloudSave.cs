using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using System.Threading.Tasks;


public class CloudSave : SaveBase
{

    public  async  Task<SaveData> OnLoadAsync()
    {
        var load = await CloudSaveService.Instance.Data.Player.LoadAsync
            (
                new HashSet<string>()
                {
                    SaveData.KEY_CURRENCY
                }
            );

        SaveData Newdata = new SaveData();
        if(load.TryGetValue(SaveData.KEY_CURRENCY,out var value))
        {
            Newdata.currency = value.Value.GetAs<int>();

        }
        return Newdata;
    }

    protected override async void OnSave(SaveData data)
    {

        Dictionary<string, object> currencyData = new Dictionary<string, object>();
        currencyData.Add(SaveData.KEY_CURRENCY, data.currency);

        await CloudSaveService.Instance.Data.Player.SaveAsync(currencyData);
    }

    protected override SaveData OnLoad()
    {
        throw new System.NotImplementedException();
    }
}
