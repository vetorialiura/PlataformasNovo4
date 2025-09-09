using UnityEngine;

public class ComMoedaState : IMachineState
{
    private SodaMachine maquina;
    public ComMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, true, true, false);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(true);
        maquina.animator.SetTrigger("ComMoeda");
        maquina.AtualizarVisualEstoque();
    }

    public void InserirMoeda() { }
    public void Cancelar()
    {
        maquina.SetEstado(maquina.estadoSemMoeda);
    }
    public void Comprar()
    {
        maquina.SetEstado(maquina.estadoVenda);
    }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}