using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SodaMachine : MonoBehaviour
{
    public Button btnInserir;
    public Button btnCancelar;
    public Button btnComprar;
    public Button btnManutencao;

    public GameObject compartimento; // Painel dentro do Canvas
    public GameObject latinhaImage;  // Para animação de venda (opcional)
    public TextMeshProUGUI avisoText;
    public Animator animator;

    public GameObject latinhaPrefab; // Prefab de UMA latinha
    public Transform latinhasContainer; // Container vazio dentro do compartimento

    public int estoque = 0;
    public int estoqueMaximo = 10; // Limite máximo de latinhas

    private IMachineState estadoAtual;
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
    public void OnManutencaoClick() => estadoAtual.Manutencao();

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
    public void MostrarLatinha(bool ativo)
    {
        if (latinhaImage != null)
            latinhaImage.SetActive(ativo);
    }

    public void AdicionarEstoque()
    {
        if (estoque < estoqueMaximo)
            estoque++;
    }

    public void RemoverEstoque()
    {
        if (estoque > 0)
            estoque--;
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
        btnManutencao.onClick.AddListener(OnManutencaoClick);
    }

    public void MostrarLatinhasEstoque()
    {
        if (latinhasContainer == null || latinhaPrefab == null) return;

        foreach (Transform child in latinhasContainer)
            UnityEngine.Object.Destroy(child.gameObject);

        for (int i = 0; i < estoque; i++)
        {
            GameObject novaLatinha = Instantiate(latinhaPrefab, latinhasContainer);
            novaLatinha.transform.localPosition = new Vector3(i * 50, 0, 0); // Ajuste 50 para espaçamento
        }
    }
}