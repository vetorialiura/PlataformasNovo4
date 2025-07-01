using UnityEngine;

public class ManutencaoState : IMachineState
{
    private SodaMachine maquina;
    public ManutencaoState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.compartimento.SetActive(true);
        maquina.AtualizarVisualEstoque();
        maquina.AtualizarAvisoManutencao();
        maquina.AtualizarInteracoes(true, false, false, true);
        maquina.animator.SetTrigger("Manutencao");
    }

    public void InserirMoeda()
    {
        maquina.estoque++;
        maquina.AtualizarVisualEstoque();
        maquina.AtualizarAvisoManutencao();
    }

    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.compartimento.SetActive(false);
        maquina.AtualizarEstado();
    }
}