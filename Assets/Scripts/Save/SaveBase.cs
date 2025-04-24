using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public abstract class SaveBase : MonoBehaviour
{

    public void Save(SaveData data)
    {
        OnSave(data);
    }

    public SaveData Load()
    {
        return OnLoad();
    }



    protected abstract  void OnSave(SaveData data);
    protected abstract SaveData OnLoad();

    //public virtual async Task<SaveData> OnLoadAsync()
    //{
    //    return Task.
    //}
}
