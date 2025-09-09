using UnityEngine;

public class SemRefrigeranteState : IMachineState
{
    private SodaMachine maquina;
    public SemRefrigeranteState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, false, false, true);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(true);
        maquina.animator.SetTrigger("SemRefrigerante");
        maquina.AtualizarAviso("VAZIO", 1, 0, 0);
        maquina.MostrarLatinhasEstoque();
    }

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}