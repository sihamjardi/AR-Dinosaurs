# **Projet AR avec Unity & Vuforia : Visualisation interactive de Velociraptors en Réalité Augmentée – Rapport**

## **Introduction**

Ce projet consiste à développer une application de Réalité Augmentée (AR) utilisant **Unity 2022.3** et **Vuforia Engine**.
L’objectif est de détecter une image (Image Target) pour afficher un Velociraptor animé et jouer des sons automatiquement lorsque la cible est reconnue.

L’application est destinée à fonctionner sur **Android**.

---

## **1. Technologies utilisées**

* Unity 2022.3 LTS
* Vuforia Engine 10+
* Android Build Support
* C# (scripts Unity)
* IL2CPP + ARM64
* OpenGLES3 (Graphics API)

---

## **2. Étapes de réalisation du projet**

---

### **2.1. Création du projet**

* Création d’un projet Unity (Template : **3D Core**)
* Installation et activation du package **Vuforia Engine**
* Ajout de la licence Vuforia dans :
  **Vuforia → Engine Configuration**

 **Capture d’écran du projet Unity créé**


<img width="570" height="142" alt="image" src="https://github.com/user-attachments/assets/4ec0bc3e-edc9-4dd2-8028-3acdc6eeac7c" />


---

### **2.2. Configuration Android**

Dans **File → Build Settings → Android** :

* Switch Platform
* Minimum API Level : **Android 10 (API Level 29)**
* Scripting Backend : **IL2CPP**
* Target Architectures : **ARM64**
* Supprimer Vulkan (ne garder que **OpenGLES3**)

 **Capture Build Settings Android**

<img width="476" height="447" alt="image" src="https://github.com/user-attachments/assets/63f918f8-11d7-4e39-a0db-2f7075f89611" />


<img width="1888" height="992" alt="image" src="https://github.com/user-attachments/assets/c096a052-1737-4605-b3c6-863609deb4a7" />


<img width="1899" height="987" alt="image" src="https://github.com/user-attachments/assets/23d9bb8e-77ab-473a-a52a-e5400768fd2a" />


<img width="1899" height="987" alt="image" src="https://github.com/user-attachments/assets/53f7ad36-f3f8-4e8b-8b27-da7f9711ee37" />


 **Other Settings – Configuration Vuforia**


<img width="594" height="416" alt="image" src="https://github.com/user-attachments/assets/9f5d9352-975e-4231-9211-1c362093f26d" />



### **2.3. Création du Image Target**

* Importation d’une image dans Vuforia Target Manager
* Création d’un **Image Target** dans Unity
* Placement du modèle 3D (Velociraptor) en enfant du target
* Ajustement de l’échelle et des animations

 **Capture du Image Target dans Unity**

<img width="206" height="73" alt="image" src="https://github.com/user-attachments/assets/55366a5c-d1ac-45f6-aca8-2b9e9c32e45e" />

<img width="343" height="411" alt="image" src="https://github.com/user-attachments/assets/457484a3-88fb-40ca-a283-cf2226744073" />

<img width="345" height="401" alt="image" src="https://github.com/user-attachments/assets/2de31295-b4c8-4598-a36a-b066afb763c4" />

<img width="344" height="438" alt="image" src="https://github.com/user-attachments/assets/ffacf8b0-671f-4311-9fb1-b7f46f18a424" />

<img width="339" height="428" alt="image" src="https://github.com/user-attachments/assets/022ac525-3689-4ca0-a383-d84ae82778d5" />

<img width="1101" height="587" alt="image" src="https://github.com/user-attachments/assets/1e56dace-8211-4721-9dae-cacbed3263f2" />

 **Placement du modèle 3D**


<img width="292" height="178" alt="image" src="https://github.com/user-attachments/assets/9af6a418-45b2-451a-8e0a-3d7cbc230332" />

<img width="862" height="588" alt="image" src="https://github.com/user-attachments/assets/60a77ca0-d51a-476b-95bf-05c677a2ac2f" />

<img width="451" height="173" alt="image" src="https://github.com/user-attachments/assets/27fffc79-daaf-471b-9334-c25b3415e686" />


### **2.4. Ajout des sons**

* Importation des fichiers sons (.wav ou .mp3)
* Ajout d’un **AudioSource** sur le modèle
* Affectation des clips audio dans l’Inspector

 **Capture AudioSource et sons assignés**

***Pour ImageTarget:***

<img width="337" height="404" alt="image" src="https://github.com/user-attachments/assets/b6c00baa-2771-44ec-b5cd-9cafeb4b489c" />


***Pour Model 1 (Dino1):***

<img width="342" height="423" alt="image" src="https://github.com/user-attachments/assets/0cd50687-213e-481a-baf3-68ce8d17f268" />


***Pour Model 2 (Dino2):***

<img width="339" height="378" alt="image" src="https://github.com/user-attachments/assets/55aa5b97-3be8-4880-ab76-8f9c500a0bde" />


---

### **2.5. Ajout des scripts**

#### **1. ARAudioManager.cs**

Permet de jouer ou arrêter les sons selon le tracking.

#### **2. DinoSounds.cs**

Utilisé pour jouer différents effets sonores (Roar, Bark, Call1, etc.)

#### **3. PlaySoundOnDetect.cs**

Active/désactive automatiquement :

* animations
* sons

selon la détection de l’image.

 **Capture des scripts dans Unity**

<img width="868" height="222" alt="image" src="https://github.com/user-attachments/assets/ce6d5fea-63af-46be-9bce-6c4f509f01e1" />


``` ARAudioManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARAudioManager : MonoBehaviour
{
    public AudioSource dino1Audio;     //audio dino1
    public AudioSource dino2Audio;     // audio dino2
    public AudioSource velcoInfoAudio; // audio Velcoinfo

    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool visible = status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED;

        if (dino1Audio)
        {
            if (visible && !dino1Audio.isPlaying) dino1Audio.Play();
            else if (!visible && dino1Audio.isPlaying) dino1Audio.Stop();
        }

        if (dino2Audio)
        {
            if (visible && !dino2Audio.isPlaying) dino2Audio.Play();
            else if (!visible && dino2Audio.isPlaying) dino2Audio.Stop();
        }

        if (velcoInfoAudio)
        {
            if (visible && !velcoInfoAudio.isPlaying) velcoInfoAudio.Play();
            else if (!visible && velcoInfoAudio.isPlaying) velcoInfoAudio.Stop();
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }
}

```

``` DinoSounds.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip roarClip;
    public AudioClip barkClip;
    public AudioClip call1Clip;
    public AudioClip call2Clip;
    public AudioClip call3Clip;

    public void Roar()
    {
        PlaySound(roarClip);
    }

    public void Bark()
    {
        PlaySound(barkClip);
    }

    public void Call1()
    {
        PlaySound(call1Clip);
    }

    public void Call2()
    {
        PlaySound(call2Clip);
    }

    public void Call3()
    {
        PlaySound(call3Clip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

```

``` PlaySoundOnDetect.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaySoundOnDetect : MonoBehaviour
{
    private AudioSource audioSource;
    private ObserverBehaviour observerBehaviour;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Disable animator and audio at start (no sound or animation yet)
        if (animator) animator.enabled = false;
        if (audioSource) audioSource.enabled = false;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        bool isVisible = targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED;

        if (isVisible)
        {
            if (animator) animator.enabled = true;
            if (audioSource)
            {
                audioSource.enabled = true;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
        }
        else
        {
            if (animator) animator.enabled = false;
            if (audioSource)
            {
                audioSource.Stop();
                audioSource.enabled = false;
            }
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
```

---

### **2.6. Tests sur mobile**

* Build APK
* Installation sur smartphone Android
* Test de la détection du marqueur
* Vérification animations + sons

 **Capture test sur smartphone **


![dino ‐ Réalisée avec Clipchamp-2](https://github.com/user-attachments/assets/0ea3bc57-3e3b-4aef-9b2c-e70718761b1b)

![dino ‐ Réalisée avec Clipchamp-1](https://github.com/user-attachments/assets/405d6831-3c78-42e6-9662-bb4131135f4e)

![dino ‐ Réalisée avec Clipchamp-3](https://github.com/user-attachments/assets/cf7347ef-5cb4-4169-b448-b6835589ad60)


---

## **3. Résultats**

L’application fonctionne correctement :

* Détection rapide du marqueur
* Apparition du Velociraptor en 3D
* Sons joués uniquement lorsque la cible est visible
* Animation activée/désactivée selon le tracking
* APK compatible Android API 29+

 **Capture du Velociraptor en AR**

<img width="469" height="299" alt="image" src="https://github.com/user-attachments/assets/defbc4db-30dd-42b9-9a19-ef4abc15d24c" />



---

## **4. Vidéo démonstrative**

Une vidéo est jointe montrant :

* Détection de la cible
* Apparition du Velociraptor
* Sons déclenchés
* Perte de tracking (arrêt du son + animation)

 **Miniature ou capture de la vidéo**


[![Vidéo démonstrative](https://img.youtube.com/vi/j1pCxNkvVwA/maxresdefault.jpg)](https://youtu.be/j1pCxNkvVwA)


---

## **5. Conclusion**

Ce projet a permis de :

* Comprendre la chaîne de fonctionnement de Vuforia
* Gérer la détection d’images
* Intégrer sons + animations
* Exporter une application AR fonctionnelle sur Android

L’application finale est interactive, fluide et démontre efficacement les bases de la Réalité Augmentée.

---

## **Structure recommandée sur GitHub**

```
📦 Projet-AR-Velociraptor
 ┣ 📁 Assets
 ┣ 📁 Scripts
 ┣ 📁 images
 ┃ ┣ project_creation.png
 ┃ ┣ build_settings.png
 ┃ ┣ image_target.png
 ┃ ┣ model_3d.png
 ┃ ┣ audio_source.png
 ┃ ┣ scripts.png
 ┃ ┣ test_mobile.png
 ┃ ┗ dino_ar.png
 ┣ README.md
 ┗ video_demo.mp4
```

