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
    [SerializeField] private AudioSource explosionClip;

    [Header("Variables")]

    [SerializeField] private float radius = 1f;
    [SerializeField] private float explosionForce = 1000f;
    [Range(1,5),SerializeField] private int multiplier;

    private bool hasExploded = false;
    private float countdown;

    #endregion

    #region BuiltIn Methods
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }
    #endregion

    #region Custom Methods

    public void Explode()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius , blastingMask);

        foreach (Collider2D nearbyGameobject in col)
        {
            Rigidbody2D rb2d = nearbyGameobject.GetComponent<Rigidbody2D>();

            if((rb2d != null))
            {
                tNTrb2D.bodyType = RigidbodyType2D.Kinematic;
                blast.Play();
                //explosionClip.Play();
                tNTSprite.enabled = false;
                box2D.enabled = false;
                Destroy(gameObject, blast.main.duration * 2);
                Vector2 dir = rb2d.transform.position - transform.position;
                if (Physics2D.Raycast(transform.position, dir, blastingMask))
                {
                    rb2d.AddForce(dir* multiplier * explosionForce);
                }
            }
        }
    }

    #endregion
}
