using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class SodaMachine : MonoBehaviour
{
    public DebugUI.Button btnInserir;
    public DebugUI.Button btnCancelar;
   public DebugUI.Button btnComprar;
    public DebugUI.Button btnManutencao;
    
    public Transform compartimentoVisual;
    public GameObject prefabLatinha;
    public TextMeshProUGUI avisoText;
    public GameObject latinhaImage;
    public GameObject compartimento;
    public int estoque = 0;

    private IMachineState estadoAtual;

    // Estados
    [HideInInspector] public SemMoedaState estadoSemMoeda;
    [HideInInspector] public ComMoedaState estadoComMoeda;
    [HideInInspector] public VendaState estadoVenda;
    [HideInInspector] public SemRefrigeranteState estadoVazio;
    [HideInInspector] public ManutencaoState estadoManutencao;

    void Start()
    {
        estadoSemMoeda = new SemMoedaState(this);
        estadoComMoeda = new ComMoedaState(this);
        estadoVenda = new VendaState(this);
        estadoVazio = new SemRefrigeranteState(this);
        estadoManutencao = new ManutencaoState(this);

        AtualizarEstado();
    }

    public void SetEstado(IMachineState novoEstado)
    {
        estadoAtual = novoEstado;
    }

    public void AtualizarEstado()
    {
        if (compartimento.activeSelf)
        {
            SetEstado(estadoManutencao);
        }
        else if (estoque <= 0)
        {
            SetEstado(estadoVazio);
        }
        else
        {
            SetEstado(estadoSemMoeda);
        }

        AtualizarUI();
    }

    public void AtualizarUI()
    {
        if (estadoAtual == estadoVazio)
        {
            avisoText.text = "VAZIO";
            avisoText.color = Color.red;
        }
        else if (estadoAtual == estadoComMoeda)
        {
            avisoText.text = "OK";
            avisoText.color = Color.green;
        }
        else if (estadoAtual == estadoManutencao)
        {
            avisoText.text = "MANUTENÇÃO";
            avisoText.color = Color.yellow;
        }
        else
        {
            avisoText.text = "";
        }

        latinhaImage.SetActive(false); // Esconde latinha por padrão
    }

    // Métodos chamados pelos botões
    public void InserirMoeda() => estadoAtual.InserirMoeda();
    public void Cancelar() => estadoAtual.Cancelar();
    public void Comprar() => estadoAtual.Comprar();
    public void Manutencao() => estadoAtual.Manutencao();

    // Chamado apenas no modo manutenção
    public void AdicionarRefrigerante()
    {
        if (estadoAtual == estadoManutencao)
        {
            estoque++;
            AtualizarEstado();
        }
    }
    public void AtualizarAvisoManutencao()
    {
        avisoText.text = "Estoque: " + estoque;
        avisoText.color = Color.yellow;
    }
    public void AtualizarVisualEstoque()
    {
    }
    public void AtualizarInteracoes(bool inserir, bool cancelar, bool comprar, bool manutencao)
    {
        btnInserir.interactable = inserir;
        btnCancelar.interactable = cancelar;
        btnComprar.interactable = comprar;
        btnManutencao.interactable = manutencao;
    }
}