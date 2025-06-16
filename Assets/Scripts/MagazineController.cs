using UnityEngine;

public class MagazineController : MonoBehaviour
{
    [field: SerializeField] public Magazine magazine { get; private set; }
    private GameObject magazineObject;
    private int currentBullet;
    public int CurrentBullet { get { return currentBullet; } set { currentBullet = value; CheckMagazine(); } }
    private Rigidbody rig;
    void OnEnable()
    {
        currentBullet = magazine.bulletNum;
        magazineObject = Instantiate(magazine.prefab, transform);
        rig = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        Destroy(magazineObject);
    }
    public void MagazineLoad()
    {
        rig.isKinematic = true;
    }
    public void MagazineUnLoad()
    {
        rig.isKinematic = false;
    }
    private void CheckMagazine()
    {
        if(currentBullet<=0)
        {
            Destroy(magazineObject);
            magazineObject = Instantiate(magazine.emptyPrefab, transform);
        }
    }
}
