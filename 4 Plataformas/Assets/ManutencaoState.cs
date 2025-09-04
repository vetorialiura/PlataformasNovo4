using UnityEngine; // <-- Necessário para Transform e Object!

public class ManutencaoState : IMachineState
{
    private SodaMachine maquina;
    public ManutencaoState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(true, false, false, true);
        maquina.animator.SetTrigger("Manutencao");
        maquina.compartimento.SetActive(true);
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
        maquina.compartimento.SetActive(false);
        foreach (Transform child in maquina.latinhasContainer)
            UnityEngine.Object.Destroy(child.gameObject);
        maquina.AtualizarEstadoBaseadoNoEstoque();
    }
}