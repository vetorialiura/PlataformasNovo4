using UnityEngine;

public class VendaState : IMachineState
{
    private SodaMachine maquina;
    public VendaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtualizarInteracoes(false, false, false, false);
        maquina.animator.SetTrigger("Venda");
        // A latinha já é mostrada no estado anterior (ComMoeda)
        // ou pode ser animada aqui também
    }

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao() { }
}