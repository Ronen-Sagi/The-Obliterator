using UnityEngine;

public class TutorialMove : MonoBehaviour
{
    [SerializeField] private int dotNum = 1;
    [SerializeField] private TutorialManager tutorialManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        if (tutorialManager == null){}
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialManager.OnDotTouched(dotNum);
            Destroy(gameObject);
        }
    }
    
}
