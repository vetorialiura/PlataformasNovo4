using UnityEngine;

public class SemMoedaState : IMachineState
{
    private SodaMachine maquina;
    public SemMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(true, false, false, true);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(true); // Sempre mostra compartimento/vidro
        maquina.animator.SetTrigger("SemMoeda");
        maquina.AtualizarVisualEstoque();
    }

    public void InserirMoeda()
    {
        maquina.SetEstado(maquina.estadoComMoeda);
    }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}