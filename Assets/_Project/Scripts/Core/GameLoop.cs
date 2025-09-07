using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [SerializeField] UIController ui;
    [SerializeField] UILog log;
    [SerializeField] Hero hero;
    [SerializeField] MonoBehaviour intentResolverBehaviour; // debe implementar IIntentResolver
    [SerializeField] MonoBehaviour consequenceRuleBehaviour; // debe implementar IConsequenceRule
    [SerializeField] MonoBehaviour authorBehaviour; // debe implementar IAuthor
    [SerializeField] MonoBehaviour rngBehaviour; // debe implementar IRNG
    public float tickSeconds = 2.5f;

    private IIntentResolver intentResolver;
    private IConsequenceRule consequenceRule;
    private IAuthor author;
    private IRNG rng;

    private Coroutine tickRoutine;
    private WaitForSecondsRealtime wait;
    private int tickCount = 0;
    public event System.Action OnTick;

    private void Awake()
    {
        intentResolver = intentResolverBehaviour as IIntentResolver;
        consequenceRule = consequenceRuleBehaviour as IConsequenceRule;
        author = authorBehaviour as IAuthor;
        rng = rngBehaviour as IRNG;

        wait = new WaitForSecondsRealtime(tickSeconds);
    }

    private void Start()
    {
        ui.Refresh(hero);
        if (tickRoutine == null) tickRoutine = StartCoroutine(TickLoop());
    }

    private void OnDisable()
    {
        if (tickRoutine != null)
        {
            StopCoroutine(tickRoutine);
            tickRoutine = null;
        }
    }

    IEnumerator TickLoop()
    {
        while (true)
        {
            yield return wait;
            ResolveTick();
        }
    }

    private void ResolveTick()
    {
        tickCount++;
        log.Append($"Tick {tickCount} @ {System.DateTime.Now.ToString("HH:mm:ss")}");
        OnTick?.Invoke();
    }
}
