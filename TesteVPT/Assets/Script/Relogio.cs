using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Relogio : MonoBehaviour
{
    [Header("UI Text com Hora Minuto Dia Mes Ano")]

    public TextMeshProUGUI timer;

    [Header("Data e Hora do Inicio")]

    [Range(0, 23)]
    public int horaAtual = 12;
    [Range(0, 59)]
    public int minutoAtual = 0;
    [Range(1, 31)]
    public int diaAtual = 28;
    [Range(1, 12)]
    public int MesAtual = 3;
    [Range(1980, 2100)]
    public int anoAtual = 2015;
    [Header("Velocidade do Relogio")]
    public float velocidadeDoRelogio = 10f;
    private float contador = 0;

    void FixedUpdate()
    {
        if(horaAtual < 10){
            timer.text = "0" + horaAtual.ToString() + ":" + minutoAtual.ToString();
        }
        if(minutoAtual < 10){
            timer.text = horaAtual.ToString() + ":0" + minutoAtual.ToString();
        }
        if(horaAtual < 10 && minutoAtual < 10){
            timer.text = "0" + horaAtual.ToString() + ":0" + minutoAtual.ToString();
        }
        if (horaAtual >= 10 && minutoAtual >= 10)
        {
            timer.text = horaAtual.ToString() + ":" + minutoAtual.ToString();
        }
        Timer();
    }

    void Timer()
    {
        contador += Time.deltaTime * velocidadeDoRelogio;
        if (contador >= 1)
        {
            minutoAtual = minutoAtual + 1;
            contador = 0;      
            if (minutoAtual >= 60)
            {
                horaAtual = horaAtual + 1;
                minutoAtual = 0;
                if (horaAtual >= 50)
                {
                    horaAtual = 0;
                    diaAtual = diaAtual + 1;

                    if (diaAtual >= 32)
                    {
                        diaAtual = 1;
                        MesAtual = MesAtual + 1;

                        if (MesAtual >= 13)
                        {
                            MesAtual = 1;
                            anoAtual = anoAtual + 1;
                        }
                    }
                }
            }
        }
    }
}
