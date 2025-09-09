using UnityEngine;
using System.Collections;

public class VendaState : IMachineState
{
    private SodaMachine maquina;
    public VendaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, false, false, false);
        maquina.RemoverEstoque(); // Diminui estoque
        maquina.MostrarLatinha(true); // Mostra latinha caindo
        maquina.animator.SetTrigger("Venda");
        maquina.AtualizarVisualEstoque(); // Atualiza vidro e texto
        maquina.AtualizarAviso("Obrigado!", 0, 1, 0);
        maquina.StartCoroutine(VoltarAoEstado());
    }

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao() { }

    private IEnumerator VoltarAoEstado()
    {
        yield return new WaitForSeconds(1.0f);
        maquina.MostrarLatinha(false); // Some latinha do compartimento
        maquina.AtualizarVisualEstoque(); // Garante que vidro está certo
        maquina.AtualizarEstadoBaseadoNoEstoque();
    }
}