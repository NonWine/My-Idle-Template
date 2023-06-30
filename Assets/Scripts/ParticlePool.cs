using System;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance;

    [SerializeField] private ParticleSystem[] bloodHitFx;
    [SerializeField] private ParticleSystem[] fireArrow;
    [SerializeField] private ParticleSystem[] explosionFx;
    [SerializeField] private ParticleSystem[] frozenExplosiveFx;
    [SerializeField] private ParticleSystem[] deadZombieFx;
    [SerializeField] private ParticleSystem[] polzunBloodHitFx;
    [SerializeField] private ParticleSystem[] polzunDeadZombieFx;
    private int currentBlood;
    private int currentFire;
    private int currentExplossion;
    private int currentfrozen;
    private int currentZombie;
    private int currentZombiePolzun;
    private int currentBloodPolzun;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayBloodPolzun(Vector3 pos)
    {
        polzunBloodHitFx[currentBloodPolzun].transform.position = pos;
        polzunBloodHitFx[currentBloodPolzun].Play();
        currentBloodPolzun++;
        if (currentBloodPolzun == polzunBloodHitFx.Length)
            currentBloodPolzun = 0;
    }

    public void PlayDeadPolzun(Vector3 pos)
    {
        polzunDeadZombieFx[currentZombiePolzun].transform.position = pos;
        polzunDeadZombieFx[currentZombiePolzun].Play();
        currentZombiePolzun++;
        if (currentZombiePolzun == polzunDeadZombieFx.Length)
            currentZombiePolzun = 0;
    }

    public void PlayDeadZombie(Vector3 pos)
    {
        deadZombieFx[currentZombie].transform.position = pos;
        deadZombieFx[currentZombie].Play();
        currentZombie++;
        if (currentZombie == deadZombieFx.Length)
            currentZombie = 0;
    }
    public void PlayFrozenExplose(Vector3 pos)
    {
        frozenExplosiveFx[currentfrozen].transform.position = pos;
        frozenExplosiveFx[currentfrozen].Play();
        currentfrozen++;
        if (currentfrozen == frozenExplosiveFx.Length)
            currentfrozen = 0;
    }

    public void PlayExplossion(Vector3 pos)
    {
        explosionFx[currentExplossion].transform.position = pos;
        explosionFx[currentExplossion].Play();
        currentExplossion++;
        if (currentExplossion == explosionFx.Length)
            currentExplossion = 0;
    }

    public void PlayBlood(Vector3 pos)
    {
        bloodHitFx[currentBlood].transform.position = pos;
        bloodHitFx[currentBlood].Play();
        currentBlood++;
        if (currentBlood == bloodHitFx.Length)
            currentBlood = 0;
    }

    public void PlayFireArrow(Vector3 pos)
    {
        fireArrow[currentFire].transform.position = pos;
        fireArrow[currentFire].Play();
        currentFire++;
        if (currentFire == bloodHitFx.Length)
            currentFire = 0;
    }
}
