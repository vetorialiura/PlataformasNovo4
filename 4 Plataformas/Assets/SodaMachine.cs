using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SodaMachine : MonoBehaviour
{
   
    
    // Botões da interface
    public Button btnInserir;
    public Button btnCancelar;
    public Button btnComprar;
    public Button btnManutencao;

    // Visual da máquina
    public GameObject compartimento;     // Compartimento das latinhas (porta)
    public GameObject latinhaImage;      // Latinha individual que aparece na venda
    public TextMeshProUGUI avisoText;    // Texto do visor
    public Animator animator;            // Animator da máquina

    public int estoque = 0;

    private IMachineState estadoAtual;
    [HideInInspector] public SemMoedaState estadoSemMoeda;
    [HideInInspector] public ComMoedaState estadoComMoeda;
    [HideInInspector] public VendaState estadoVenda;
    [HideInInspector] public SemRefrigeranteState estadoVazio;
    [HideInInspector] public ManutencaoState estadoManutencao;

    public GameObject latinhaPrefab; // Prefab para múltiplas latinhas
    public Transform latinhasContainer; // Container das latinhas

    void Start()
    {
        estadoSemMoeda = new SemMoedaState(this);
        estadoComMoeda = new ComMoedaState(this);
        estadoVenda = new VendaState(this);
        estadoVazio = new SemRefrigeranteState(this);
        estadoManutencao = new ManutencaoState(this);

        MostrarLatinha(false);
        MostrarCompartimento(false);

        if (estoque > 0)
            SetEstado(estadoSemMoeda);
        else
            SetEstado(estadoVazio);

        ConectarBotoes();
    }

    public void SetEstado(IMachineState novoEstado)
    {
        estadoAtual = novoEstado;
        estadoAtual.Entrar();
    }

    public void InserirMoeda() => estadoAtual.InserirMoeda();
    public void Cancelar() => estadoAtual.Cancelar();
    public void Comprar() => estadoAtual.Comprar();
    public void Manutencao() => estadoAtual.Manutencao();

    public void AtivarBotoes(bool inserir, bool cancelar, bool comprar, bool manutencao)
    {
        btnInserir.interactable = inserir;
        btnCancelar.interactable = cancelar;
        btnComprar.interactable = comprar;
        btnManutencao.interactable = manutencao;
    }

    public void AtualizarAviso(string texto, float r, float g, float b)
    {
        avisoText.text = texto;
        avisoText.color = new Color(r, g, b);
    }

    public void MostrarCompartimento(bool ativo) => compartimento.SetActive(ativo);
    public void MostrarLatinha(bool ativo) { if (latinhaImage != null) latinhaImage.SetActive(ativo); }

    public void AdicionarEstoque()
    {
        estoque++;
    }

    public void RemoverEstoque()
    {
        if (estoque > 0) estoque--;
    }

    public void AtualizarEstadoBaseadoNoEstoque()
    {
        MostrarLatinha(false);
        if (estoque <= 0)
            SetEstado(estadoVazio);
        else
            SetEstado(estadoSemMoeda);
    }

    void ConectarBotoes()
    {
        btnInserir.onClick.AddListener(InserirMoeda);
        btnCancelar.onClick.AddListener(Cancelar);
        btnComprar.onClick.AddListener(Comprar);
        btnManutencao.onClick.AddListener(Manutencao);
    }

    // MOSTRAR VÁRIAS LATINHAS NO MODO MANUTENÇÃO
    public void MostrarLatinhasEstoque()
    {
        if (latinhasContainer == null || latinhaPrefab == null) return;

        // Limpa latinhas antigas
        foreach (Transform child in latinhasContainer)
            Destroy(child.gameObject);

        // Instancia uma latinha para cada refrigerante no estoque
        for (int i = 0; i < estoque; i++)
        {
            GameObject novaLatinha = Instantiate(latinhaPrefab, latinhasContainer);
            novaLatinha.transform.localPosition = new Vector3(i * 0.5f, 0, 0); // ajuste o espaçamento se desejar
        }
    }
}