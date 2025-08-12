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
        maquina.MostrarLatinhasEstoque();
        maquina.AtualizarAviso($"ESTOQUE: {maquina.estoque}", 1, 1, 0);
    }

    public void InserirMoeda()
    {
        maquina.AdicionarEstoque();
        maquina.MostrarLatinhasEstoque();
        maquina.AtualizarAviso($"ESTOQUE: {maquina.estoque}", 1, 1, 0);
    }

    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.MostrarCompartimento(false);
        foreach (Transform child in maquina.latinhasContainer)
            GameObject.Destroy(child.gameObject);
        maquina.AtualizarEstadoBaseadoNoEstoque();

       
        
    }
    
}