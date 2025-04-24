using UnityEngine;

public class Rifle : Weapon
{
    public Factory<Bullets> BulletFactory;
    public ParticleSystem particlesSpark;
    public ParticleSystem particlesSmoke;

    [SerializeField] AudioClip fireSoundClip;
    

    void Start()
    {
        BulletFactory = FindAnyObjectByType<BulletFactory>();
    }
    public override void Shoot()
    {
        var b = BulletFactory.Create();
        b.transform.position = transform.position;
        b.transform.forward = transform.forward;
        SoundFXManager.instance.PlaySoundFXClip(fireSoundClip, transform, 1f);
        

        var spark = Instantiate(particlesSpark, transform.position, Quaternion.identity);
        var smoke = Instantiate(particlesSmoke, transform.position, Quaternion.identity);

        // Destruir las part�culas despu�s de 2 segundos (ajustable)
        Destroy(spark.gameObject, 2f);  // 2 segundos de duraci�n para las part�culas de chispas
        Destroy(smoke.gameObject, 2f);  // 2 segundos de duraci�n para las part�culas de humo
    }
}

