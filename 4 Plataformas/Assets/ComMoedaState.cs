using UnityEngine;

public class ComMoedaState : IMachineState
{
    private SodaMachine maquina;
    public ComMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtualizarInteracoes(false, true, true, false);
        maquina.animator.SetTrigger("ComMoeda");
    }

    public void InserirMoeda() { } // Não pode inserir mais
    public void Cancelar()
    {
        maquina.SetEstado(maquina.estadoSemMoeda);
    }

    public void Comprar()
    {
        maquina.SetEstado(maquina.estadoVenda);
        maquina.estoque--;
        maquina.latinhaImage.SetActive(true);
        maquina.Invoke(nameof(maquina.AtualizarEstado), 1f);
    }

    public void Manutencao() { }
}