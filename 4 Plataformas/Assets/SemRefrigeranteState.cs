public class SemRefrigeranteState : IMachineState
{
    private SodaMachine maquina;
    public SemRefrigeranteState(SodaMachine m) => maquina = m;

    public void Entrar()
    {
        maquina.AtivarBotoes(false, false, false, true);
        maquina.AtualizarAviso("VAZIO", 1, 0, 0);
        maquina.MostrarLatinha(false);
        maquina.MostrarCompartimento(false);
        maquina.animator.SetTrigger("SemRefrigerante");
    }

    public void InserirMoeda() { }
    public void Cancelar() { }
    public void Comprar() { }
    public void Manutencao()
    {
        maquina.SetEstado(maquina.estadoManutencao);
    }
}