using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    Func<T>  _Factory;
    Action<T> _TurnOn, _TurnOff;
    List<T> _stockAviable = new ();
    public ObjectPool(Func<T> Factory,Action<T> TurnOn, Action<T> TurnOff, int inicialstock)
    {
        _Factory = Factory;
        _TurnOn = TurnOn;
        _TurnOff = TurnOff;

        for (int i = 0; i < inicialstock; i++)
        {
            var x = _Factory();

            TurnOff(x);
            _stockAviable.Add(x);
        }
    }

    public T Get()
    {
        if (_stockAviable.Count > 0)
        {
            var x = _stockAviable[0];
            _stockAviable.Remove(x);
            _TurnOn(x);
            return x;
        }

        return _Factory();
    }
    public void Return(T value)
    {
        
        _TurnOff(value);
        _stockAviable.Add(value);
        
    }
}
