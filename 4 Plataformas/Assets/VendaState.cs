using UnityEngine;

public class VendaState : IMachineState
{
    private SodaMachine maquina;

    public VendaState(SodaMachine m) => maquina = m;

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao() { }
}
