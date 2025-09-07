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
}
