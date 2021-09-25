using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    #region Variables

    [Header("Components")]

    [SerializeField] private ParticleSystem blast;
    [SerializeField] private SpriteRenderer tNTSprite;
    [SerializeField] private Rigidbody2D tNTrb2D;
    [SerializeField] private BoxCollider2D box2D;
    [SerializeField] private LayerMask blastingMask;

    [Header("Variables")]

    [SerializeField] private float delay = 3f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float explosionForce = 100f;

    private bool hasExploded = false;
    private float countdown;

    #endregion

    #region BuiltIn Methods

    private void Awake()
    {
        blast = GetComponent<ParticleSystem>();
        tNTSprite = GetComponent<SpriteRenderer>();
        tNTrb2D = GetComponent<Rigidbody2D>();
        box2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    #endregion

    #region Custom Methods

    public void Explode()
    {
        blast.Play();
        tNTSprite.enabled = false;
        tNTrb2D.bodyType = RigidbodyType2D.Kinematic;
        box2D.enabled = false;
        Destroy(gameObject, blast.main.duration * 2);

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius , blastingMask);

        foreach (Collider2D nearbyGameobject in col)
        {
            Rigidbody2D rb2d = nearbyGameobject.GetComponent<Rigidbody2D>();

            if((rb2d != null))
            {
                Vector2 dir = rb2d.transform.position - transform.position;
                if (Physics2D.Raycast(transform.position, dir, blastingMask))
                {
                    rb2d.AddForce(dir * explosionForce);
                }
            }
        }
    }

    #endregion
}
