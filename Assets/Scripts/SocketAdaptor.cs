using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketAdaptor : MonoBehaviour
{
    private Gun gun;
    private XRSocketInteractor interactor;
    private void Awake()
    {
        gun = GetComponentInParent<Gun>();
        interactor = GetComponent<XRSocketInteractor>();
        interactor.selectEntered.AddListener(OnMagazineLoaded);
        interactor.selectExited.AddListener(OnMagazineUnLoaded);
    }

    void OnMagazineLoaded(SelectEnterEventArgs args)
    {
        MagazineController mag = args.interactableObject.transform.GetComponent<MagazineController>();
        if (mag != null&&gun!=null)
        {
            gun.magazineLoad(mag);
        }
    }
    void OnMagazineUnLoaded(SelectExitEventArgs args)
    {
        if(gun!=null)
        {
            gun.magazineUnLoad();
        }
    }
}
