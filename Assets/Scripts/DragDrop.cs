using System;
using UnityEngine;


public class DragDrop : MonoBehaviour
{
    private Vector2 mousePosition;
    
    private bool dragging = false;
    private bool OnFile = false;
    private bool win = false;
    [SerializeField] private Spawning spawning;
    [SerializeField] private AudioSource dechire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawning = FindAnyObjectByType<Spawning>();
        GameObject a = GameObject.FindGameObjectWithTag("alldocu");
        dechire = a.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && dragging && OnFile)
        {
            if (win) spawning.score += 10;
            else spawning.score -= 10;
            dechire.Play();
            Destroy(gameObject);
        }
    }

    private Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        
        Vector2 a = Input.mousePosition;
        mousePosition = a - GetMousePosition();
    }
    

    private void OnMouseDrag()
    {
        dragging = true;
        Vector2 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = a;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnFile = true;
        if (other.gameObject.CompareTag(gameObject.tag)) win = true; ;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {   
        
        OnFile = false;
        win = false;
    }
}
