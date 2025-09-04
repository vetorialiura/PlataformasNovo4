public class ComMoedaState : IMachineState
{
    private SodaMachine maquina;
    public ComMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, true, true, false);
        maquina.AtualizarAviso("OK", 0, 1, 0);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(false);
        maquina.animator.SetTrigger("ComMoeda");
    }

    public void InserirMoeda() { }
    public void Cancelar()
    {
        maquina.SetEstado(maquina.estadoSemMoeda);
    }
    public void Comprar()
    {
        maquina.SetEstado(maquina.estadoVenda);
    }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}