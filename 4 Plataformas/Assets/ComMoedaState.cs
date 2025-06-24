using UnityEngine;

public class ComMoedaState : IMachineState
{
    private SodaMachine maquina;

    public ComMoedaState(SodaMachine m) => maquina = m;

    public void InserirMoeda() { } // Não pode inserir mais
    public void Cancelar()
    {
        maquina.SetEstado(maquina.estadoSemMoeda);
        maquina.AtualizarUI();
    }

    public void Comprar()
    {
        maquina.SetEstado(maquina.estadoVenda);
        maquina.estoque--;
        maquina.latinhaImage.SetActive(true);
        maquina.Invoke(nameof(maquina.AtualizarEstado), 1f); // Delay pra simular a venda
    }

    public void Manutencao() { } // Bloqueado
}
