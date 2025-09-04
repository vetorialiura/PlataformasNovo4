using UnityEngine;
using System.Collections;

public class VendaState : IMachineState
{
    private SodaMachine maquina;
    public VendaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, false, false, false);
        maquina.RemoverEstoque();
        maquina.MostrarLatinha(true); // Latinha animada "saindo"
        maquina.AtualizarAviso("OBRIGADO!", 0, 1, 0);
        maquina.animator.SetTrigger("Venda");
        maquina.StartCoroutine(VoltarAoEstado());
    }
    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao() { }

    private IEnumerator VoltarAoEstado()
    {
        yield return new WaitForSeconds(1.0f);
        maquina.MostrarLatinha(false); // Esconde latinha animada
        maquina.AtualizarAviso($"ESTOQUE: {maquina.estoque}", 1, 1, 0); // Atualiza texto do estoque
        maquina.MostrarLatinhasEstoque(); // Atualiza container visual
        maquina.AtualizarEstadoBaseadoNoEstoque();
    }
}