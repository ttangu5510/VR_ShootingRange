using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool hasHit;

    [Header("Set Particle")]
    [SerializeField] ParticleSystem hitStone;
    [SerializeField] ParticleSystem hitMetal;

    [Header("Set Audio")]
    [SerializeField] AudioSource hitStoneSound;
    [SerializeField] AudioSource hitMetalSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        ContactPoint[] conts = new ContactPoint[collision.contactCount];
        collision.GetContacts(conts);

        foreach (var contact in conts)
        {
            GameObject hitObject = contact.otherCollider.gameObject;
            
            if (hitObject.layer == 10)
            {
                Instantiate(hitStone, contact.point, Quaternion.LookRotation(contact.normal));
                AudioSource.PlayClipAtPoint(hitStoneSound.clip, contact.point);
            }
            else if (hitObject.layer == 11)
            {
                Instantiate(hitMetal, contact.point, Quaternion.LookRotation(contact.normal));
                AudioSource.PlayClipAtPoint(hitMetalSound.clip, contact.point);
            }
            
            Destroy(gameObject);
            break;
        }
    }
}
