using UnityEngine;

public class SemRefrigeranteState : IMachineState
{
    private SodaMachine maquina;

    public SemRefrigeranteState(SodaMachine m) => maquina = m;

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.compartimento.SetActive(true);
        maquina.AtualizarEstado();
    }
}
