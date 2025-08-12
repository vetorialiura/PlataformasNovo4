using UnityEngine;
using System.Collections;

public class VendaState : IMachineState
{
    private SodaMachine maquina;
    public VendaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, false, false, false); // Todos inativos
        maquina.RemoverEstoque();
        maquina.MostrarLatinha(true);
        maquina.AtualizarAviso("OBRIGADO!", 0, 1, 0);
        maquina.StartCoroutine(VoltarAoEstado());
    }

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }

    private IEnumerator VoltarAoEstado()
    {
        yield return new WaitForSeconds(1.0f);
        maquina.AtualizarEstadoBaseadoNoEstoque();
    }
}