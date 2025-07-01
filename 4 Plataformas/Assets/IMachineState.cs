using UnityEngine;

public interface IMachineState 
{
    void InserirMoeda();
    void Cancelar();
    void Comprar();
    void Manutencao();
    
    void Entrar();
}
