using UnityEngine;

    public class SemMoedaState : IMachineState
    {
        private SodaMachine maquina;

        public SemMoedaState(SodaMachine m) => maquina = m;

        public void InserirMoeda()
        {
            maquina.SetEstado(maquina.estadoComMoeda);
            maquina.AtualizarUI();
        }

        public void Cancelar() { }
        public void Comprar() { }

        public void Manutencao()
        {
            maquina.compartimento.SetActive(true);
            maquina.AtualizarEstado();
        }
    }

