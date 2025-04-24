
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public TextMeshProUGUI text_stamina;
    public TextMeshProUGUI text_time;

    const string CURRENT = "current";
    const string NEXT = "next";

    public int stamina = 3;
    int maxStamina = 3;

    float time_to_recharge = 30f;

    DateTime next;

    Coroutine coroutine;

    private void Update()
    {


        TimeSpan left = DateTime.Now - next;
        text_time.text = PrettyTime(next) + "\n" + PrettyTime(left);
    }


    private void Start()
    {
        
        Load();
        // cargo stamina, y next time desde el disco duro
       
            coroutine = StartCoroutine(UpdateStamina());
        
        
        Refresh();
    }
    IEnumerator UpdateStamina()
    {
        Debug.Log("Starting UpdateStamina Coroutine");

        while (stamina < maxStamina)
        {
            while (DateTime.Now > next)
            {
                Debug.Log("Adding stamina...");
                AddStamina(1);
            }

            Refresh();
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Coroutine finished");
        coroutine = null; // Limpieza explícita al finalizar
    }



    void Refresh()
    {
        text_stamina.text = stamina.ToString() + "/" + maxStamina.ToString();
        Save();

        Debug.Log("Stamina refreshed: " + stamina + "/" + maxStamina);
    }

    public void UseStamina(int quant)
    {
        if (stamina - quant >= 0)
        {
            stamina -= quant;
        }

        if (stamina < maxStamina) // Tengo que rellenar stamina
        {
            if (DateTime.Now > next) // Solo si no tenía un progreso de tiempo
            {
                next = DateTime.Now.AddSeconds(time_to_recharge);
            }

            if (coroutine == null) // Solo iniciar si no hay una coroutine activa
            {
                coroutine = StartCoroutine(UpdateStamina());
            }
        }

        Refresh();
    }


    public void AddStamina(int quant)
    {
        stamina += quant;

        if (stamina >= maxStamina) // Ya tengo el máximo de stamina
        {
            stamina = maxStamina;
            next = DateTime.Now;
            Delete();

            if (coroutine != null) // Solo detén la coroutine si no es null
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
        else
        {
            if (DateTime.Now > next)
            {
                next = next.AddSeconds(time_to_recharge);
            }
            else
            {
                next = DateTime.Now.AddSeconds(time_to_recharge);
            }
        }

        Refresh();
    }

    string PrettyTime(DateTime date)
    {
        return date.Minute.ToString("00") + ":" + date.Second.ToString("00");
    }
    string PrettyTime(TimeSpan date)
    {
        return date.Minutes.ToString("00") + ":" + date.Seconds.ToString("00");
    }


    void Load()
    {
        stamina = PlayerPrefs.GetInt(CURRENT, maxStamina);
        string date = PlayerPrefs.GetString(NEXT);
        if (String.IsNullOrEmpty(date))
            next = DateTime.Now;
        else
            next = DateTime.Parse(date);


  
    }
    void Save()
    {
        PlayerPrefs.SetInt(CURRENT, stamina);
        PlayerPrefs.SetString(NEXT, next.ToString());
    }

    void Delete()
    {
        PlayerPrefs.DeleteKey(CURRENT);
        PlayerPrefs.DeleteKey(NEXT);
    }

}
