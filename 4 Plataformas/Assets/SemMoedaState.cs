using UnityEngine;

public class SemMoedaState : IMachineState
{
    private SodaMachine maquina;
    public SemMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtualizarInteracoes(true, false, false, true);
        maquina.animator.SetTrigger("SemMoeda");
    }

    public void InserirMoeda()
    {
        maquina.SetEstado(maquina.estadoComMoeda);
    }

    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.compartimento.SetActive(true);
        maquina.AtualizarEstado();
    }
}
