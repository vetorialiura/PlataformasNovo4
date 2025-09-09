using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SodaMachine : MonoBehaviour
{
    public Button btnInserir;
    public Button btnCancelar;
    public Button btnComprar;
    public Button btnManutencao;

    public GameObject compartimento; // Painel do compartimento (Canvas)
    public GameObject latinhaImage;  // Latinha animada da entrega
    public TextMeshProUGUI avisoText;
    public Animator animator;

    public GameObject latinhaPrefab; // Prefab de UMA latinha, para o vidro
    public Transform latinhasContainer; // Container no vidro

    public int estoque = 10;
    public int estoqueMaximo = 10;

    private IMachineState estadoAtual;
    public SemMoedaState estadoSemMoeda;
    public ComMoedaState estadoComMoeda;
    public VendaState estadoVenda;
    public SemRefrigeranteState estadoVazio;
    public ManutencaoState estadoManutencao;

    void Start()
    {
        estadoSemMoeda = new SemMoedaState(this);
        estadoComMoeda = new ComMoedaState(this);
        estadoVenda = new VendaState(this);
        estadoVazio = new SemRefrigeranteState(this);
        estadoManutencao = new ManutencaoState(this);

        MostrarLatinha(false);
        MostrarCompartimento(true);

        if (estoque > 0)
            SetEstado(estadoSemMoeda);
        else
            SetEstado(estadoVazio);

        ConectarBotoes();
        AtualizarVisualEstoque();
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

    // Atualiza latinhas no vidro e texto do estoque
    public void AtualizarVisualEstoque()
    {
        MostrarLatinhasEstoque();
        AtualizarAviso($"ESTOQUE: {estoque}", 1, 1, 1);
    }

    // AJUSTADO: latinhas em grade, 4 por linha!
    public void MostrarLatinhasEstoque()
    {
        if (latinhasContainer == null || latinhaPrefab == null) return;

        foreach (Transform child in latinhasContainer)
            UnityEngine.Object.Destroy(child.gameObject);

        int maxPorLinha = 4;        // 4 latinhas por linha
        int espacamentoX = 50;      // pixels horizontal. Ajuste conforme prefabs
        int espacamentoY = -60;     // pixels vertical (negativo = desce)

        for (int i = 0; i < estoque; i++)
        {
            int coluna = i % maxPorLinha;
            int linha = i / maxPorLinha;
            GameObject novaLatinha = Instantiate(latinhaPrefab, latinhasContainer);
            novaLatinha.transform.localPosition = new Vector3(coluna * espacamentoX, linha * espacamentoY, 0);
        }
    }
}