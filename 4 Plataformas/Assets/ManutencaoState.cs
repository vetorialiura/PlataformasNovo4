using UnityEngine;

public class ManutencaoState : IMachineState
{
    private SodaMachine maquina;

    public ManutencaoState(SodaMachine m) => maquina = m;

    public void InserirMoeda()
    {
        maquina.estoque++;
        maquina.AtualizarVisualEstoque();      // se estiver usando imagens de latinhas
        maquina.AtualizarAvisoManutencao();    // atualiza o visor com "Estoque: X"
    }

    public void Entrar()
    {
        maquina.compartimento.SetActive(true);
        maquina.AtualizarVisualEstoque();
        maquina.AtualizarAvisoManutencao();    // mostra estoque logo que entra na manutenção
        maquina.AtualizarInteracoes(true, false, false, true);
    }
    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.compartimento.SetActive(false);
        maquina.AtualizarEstado();
    }
}
