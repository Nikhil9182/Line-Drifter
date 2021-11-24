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
    [SerializeField] private LayerMask nearbyTNTMask;
    [SerializeField] private AudioSource explosionClip;

    [Header("Variables")]

    [SerializeField] private float directBlastRadius = 3.5f;
    [SerializeField] private float explosionForce = 1000f;
    [Range(1,5),SerializeField] private int multiplier;

    bool willDirectBlast = false;

    #endregion

    #region BuiltIn Methods
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, directBlastRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Car") || collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("TNT"))
        {
            Explode(false);
        }
    }
    #endregion

    #region Custom Methods

    public void Explode(bool exp)
    {
        willDirectBlast = exp;

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, directBlastRadius , blastingMask);

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
                willDirectBlast = true;
                Destroy(gameObject, blast.main.duration * 2);

                Vector2 dir = rb2d.transform.position - transform.position;
                if (Physics2D.Raycast(transform.position, dir, blastingMask))
                {
                    rb2d.AddForce(dir * multiplier * explosionForce);
                }
            }
        }

        if(willDirectBlast)
        {
            Collider2D[] collTNT = Physics2D.OverlapCircleAll(transform.position, directBlastRadius, nearbyTNTMask);

            foreach (Collider2D nearbyTNT in collTNT)
            {
                if (nearbyTNT)
                {
                    StartCoroutine(ExplodingDelay(nearbyTNT));
                }
            }
        }
    }

    IEnumerator ExplodingDelay(Collider2D nearbyTNT)
    {
        yield return new WaitForSeconds(0.05f);
        tNTrb2D.bodyType = RigidbodyType2D.Kinematic;
        blast.Play();
        //explosionClip.Play();
        tNTSprite.enabled = false;
        box2D.enabled = false;
        Destroy(gameObject, blast.main.duration * 2);
        //Debug.Log(nearbyTNT.name);
        nearbyTNT.GetComponent<Explosive>().Explode(true);
    }

    #endregion
}
