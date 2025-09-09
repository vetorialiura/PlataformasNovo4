using UnityEngine;

public class ManutencaoState : IMachineState
{
    private SodaMachine maquina;
    public ManutencaoState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(true, false, false, true);
        maquina.animator.SetTrigger("Manutencao");
        maquina.MostrarCompartimento(true);
        maquina.AtualizarVisualEstoque();
        maquina.AtualizarAviso("Modo Manuten��o", 1, 1, 0);
    }

    public void InserirMoeda()
    {
        maquina.AdicionarEstoque();
        maquina.AtualizarVisualEstoque();
    }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao()
    {
        maquina.AtualizarVisualEstoque();
        maquina.AtualizarEstadoBaseadoNoEstoque();
    }
}