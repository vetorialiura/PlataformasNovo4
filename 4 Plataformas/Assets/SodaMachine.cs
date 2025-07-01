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
    public GameObject latinhaImage;      // Latinha que aparece na venda
    public TextMeshProUGUI avisoText;    // Texto do visor
    public Animator animator;            // Animator da máquina

    [HideInInspector] public int estoque = 0; // Estoque de latinhas

    private IMachineState estadoAtual;
    [HideInInspector] public SemMoedaState estadoSemMoeda;
    [HideInInspector] public ComMoedaState estadoComMoeda;
    [HideInInspector] public VendaState estadoVenda;
    [HideInInspector] public SemRefrigeranteState estadoVazio;
    [HideInInspector] public ManutencaoState estadoManutencao;

    void Start()
    {
        // Inicializa os estados
        estadoSemMoeda = new SemMoedaState(this);
        estadoComMoeda = new ComMoedaState(this);
        estadoVenda = new VendaState(this);
        estadoVazio = new SemRefrigeranteState(this);
        estadoManutencao = new ManutencaoState(this);

        // Começa no estado correto conforme estoque
        if (estoque > 0)
            SetEstado(estadoSemMoeda);
        else
            SetEstado(estadoVazio);

        ConectarBotoes();
    }

    // Troca o estado e chama o método Entrar que dispara trigger no animator
    public void SetEstado(IMachineState novoEstado)
    {
        estadoAtual = novoEstado;
        estadoAtual.Entrar();
    }

    // Métodos chamados pelos botões (OnClick)
    public void InserirMoeda() => estadoAtual.InserirMoeda();
    public void Cancelar() => estadoAtual.Cancelar();
    public void Comprar() => estadoAtual.Comprar();
    public void Manutencao() => estadoAtual.Manutencao();

    // Esse método será chamado por Animation Events em cada animação para ativar/desativar botões
    public void AtivarBotoes(bool inserir, bool cancelar, bool comprar, bool manutencao)
    {
        btnInserir.interactable = inserir;
        btnCancelar.interactable = cancelar;
        btnComprar.interactable = comprar;
        btnManutencao.interactable = manutencao;
    }

    // Esse método será chamado por Animation Events para atualizar o texto do visor
    public void AtualizarAviso(string texto, float r, float g, float b)
    {
        avisoText.text = texto;
        avisoText.color = new Color(r, g, b);
    }

    // Controla o compartimento (porta) visível ou não (pode ser chamado por Animation Events)
    public void MostrarCompartimento(bool ativo)
    {
        compartimento.SetActive(ativo);
    }

    // Controla a latinha visível ou não (ex: aparece na venda)
    public void MostrarLatinha(bool ativo)
    {
        latinhaImage.SetActive(ativo);
    }

    // Incrementa estoque, usado pelo estado manutenção (por exemplo)
    public void AdicionarEstoque()
    {
        estoque++;
    }

    // Decrementa estoque, usado na venda
    public void RemoverEstoque()
    {
        if (estoque > 0) estoque--;
    }

    // Atualiza o estado após ações que mudam estoque ou condições
    // Aqui você deve chamar a mudança de estado conforme a lógica do seu jogo
    // Exemplo: no seu código, você pode usar o Animator para disparar transições e dentro dos estados usar SetEstado conforme necessário.
    // Para manter simples, o controle de estados deve ficar nos estados, não aqui, para evitar if-else.
    // Então deixe essa função para você chamar SetEstado quando quiser trocar estado.
    public void AtualizarEstadoBaseadoNoEstoque()
    {
        if (estoque <= 0)
            SetEstado(estadoVazio);
        else
            SetEstado(estadoSemMoeda);
    }

    // Liga os botões aos métodos da máquina
    void ConectarBotoes()
    {
        btnInserir.onClick.AddListener(InserirMoeda);
        btnCancelar.onClick.AddListener(Cancelar);
        btnComprar.onClick.AddListener(Comprar);
        btnManutencao.onClick.AddListener(Manutencao);
    }
}