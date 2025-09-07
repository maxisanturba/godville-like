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

        ValidateDependencies();

        wait = new WaitForSecondsRealtime(tickSeconds);
    }

    private void ValidateDependencies()
    {
        if (hero == null) Debug.LogError("[GameLoop] hero NO asignado en el inspector.");
        if (ui == null) Debug.LogError("[GameLoop] ui (UIController) NO asignado en el inspector.");
        if (log == null) Debug.LogError("[GameLoop] log (UILog) NO asignado en el inspector.");

        if (intentResolverBehaviour == null)
            Debug.LogError("[GameLoop] intentResolverBehaviour NO asignado.");
        else if (intentResolver == null)
            Debug.LogError("[GameLoop] intentResolverBehaviour NO implementa IIntentResolver.");

        if (consequenceRuleBehaviour == null)
            Debug.LogError("[GameLoop] consequenceRuleBehaviour NO asignado.");
        else if (consequenceRule == null)
            Debug.LogError("[GameLoop] consequenceRuleBehaviour NO implementa IConsequenceRule.");

        if (authorBehaviour == null)
            Debug.LogError("[GameLoop] authorBehaviour NO asignado.");
        else if (author == null)
            Debug.LogError("[GameLoop] authorBehaviour NO implementa IAuthor.");
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
        // 1) Pedir intención
        var intent = intentResolver.GetNextIntent(hero);
        // 2) Resolver consecuencias (no usamos OutcomeGrade ni WorldState por ahora)
        var result = consequenceRule.Apply(intent, hero);
        // 3) Snapshot "antes" (para el Author)
        var before = new HeroSnapshot(hero);
        // 4) Aplicar al heroe (con clamp de HP)
        hero.HP = Mathf.Clamp(hero.HP + result.DeltaHP, 0, hero.HPMax);
        hero.XP += result.DeltaXP;
        hero.Gold += result.DeltaGold;
        // 5) UI
        ui.Refresh(hero);
        // 6) Log (línea narrativa + línea técnica del tick)
        string line = author.GetLogLine(before, intent, result);
        tickCount++;
        log.Append($"Tick {tickCount} @ {System.DateTime.Now.ToString("HH:mm")}: {line}");
        // 7) Evento (si alguien escucha)
        OnTick?.Invoke();
    }
}
