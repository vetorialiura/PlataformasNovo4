public class SemMoedaState : IMachineState
{
    private SodaMachine maquina;
    public SemMoedaState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(true, false, false, true); // INSERIR e MANUTENÇÃO ativados
        maquina.AtualizarAviso("INSIRA MOEDA", 1, 1, 1);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(false);
    }

    public void InserirMoeda()
    {
        maquina.SetEstado(maquina.estadoComMoeda);
    }

    public void Cancelar() { }
    public void Comprar() { }

    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}