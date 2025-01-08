###https://www.kyliwoog.be
### **Présentation de l'application : FTP Sync App**

---

#### **1. Introduction**
Lors du développement web, il est souvent nécessaire de mettre à jour des fichiers sur un serveur distant. Ces fichiers, qu'il s'agisse de feuilles de style, de scripts ou de ressources multimédias, doivent être synchronisés rapidement et efficacement. C'est pour répondre à ce besoin que l'application **FTP Sync App** a été conçue. 

**FTP Sync App** est une solution simple et efficace qui permet de synchroniser automatiquement des fichiers entre un dossier local et un serveur FTP. L’application est particulièrement utile pour les développeurs web et les équipes techniques qui travaillent avec des environnements distants nécessitant des mises à jour fréquentes.

---

#### **2. Fonctionnalités principales**

##### **2.1. Synchronisation automatique**
L’application surveille en temps réel les modifications effectuées dans les dossiers locaux. Les fichiers créés, modifiés ou supprimés sont immédiatement synchronisés avec le serveur FTP correspondant.

##### **2.2. Gestion des paires de dossiers**
L’utilisateur peut définir plusieurs paires de dossiers (local/serveur distant), chacune liée à un projet ou à une tâche spécifique. Cela permet une gestion simplifiée de plusieurs environnements.

##### **2.3. Connexion sécurisée au serveur FTP**
- Support des connexions FTP et FTPS pour assurer la sécurité des données transférées.
- Gestion des préférences par serveur FTP, permettant de sauvegarder et de restaurer les configurations rapidement.

##### **2.4. Interface utilisateur intuitive**
- **TreeView** pour naviguer facilement dans les répertoires distants.
- Liste des paires de synchronisation avec options d’ajout et de suppression.
- Logs en temps réel affichant les actions effectuées (ajout, modification, suppression), les chemins des fichiers concernés et les éventuelles erreurs.

##### **2.5. Historique et suivi des logs**
Un suivi détaillé des opérations effectuées est disponible directement dans l’application. Cela inclut :
- Les actions exécutées (synchronisation, suppression).
- Les fichiers impliqués.
- Les statuts des opérations (succès ou échec).

---

#### **3. Besoin spécifique : Développement web**
En tant que développeur web, j'avais besoin d'une solution capable de synchroniser automatiquement les fichiers de mon environnement local avec un serveur de production ou de test. L’objectif principal était de garantir que les fichiers mis à jour localement soient immédiatement disponibles sur le serveur, sans intervention manuelle.

##### **Problèmes rencontrés :**
- Processus manuel répétitif de mise à jour des fichiers via un client FTP.
- Risque d’oublier de transférer certains fichiers critiques.
- Perte de temps dans la navigation pour identifier les fichiers à synchroniser.

##### **Solution apportée par FTP Sync App :**
- Synchronisation automatique : Plus besoin de vérifier manuellement les fichiers modifiés.
- Gestion multi-projets : Possibilité de synchroniser plusieurs paires de dossiers pour différents projets web.
- Logs détaillés : Assurance que toutes les actions sont enregistrées et traçables.

---

#### **4. Processus de développement**

##### **4.1. Conception**
L’idée de l’application est née d’un besoin réel lors de projets web nécessitant des transferts fréquents entre local et distant. Une étude des outils existants a révélé un manque de solutions simples et dédiées à ce type de tâches.

##### **4.2. Technologies utilisées**
- **Langage** : C#
- **Framework** : .NET Framework 4.7.2
- **Interface utilisateur** : Windows Forms
- **Bibliothèque FTP** : [FluentFTP](https://github.com/robinrodricks/FluentFTP)
- **Gestion des fichiers** : FileSystemWatcher pour surveiller les changements locaux.

##### **4.3. Défis techniques**
- Gérer les connexions FTP/FTPS tout en garantissant la compatibilité avec différents serveurs.
- Implémenter une synchronisation automatique en temps réel, fiable et sans surcharge.
- Concevoir une interface utilisateur intuitive, adaptée à un usage professionnel.

---

#### **5. Fonctionnement de l’application**

1. **Connexion au serveur FTP :**
   - L'utilisateur configure ses identifiants FTP et sélectionne un dossier distant.
   - La connexion est testée pour garantir l'accès au serveur.

2. **Définition des paires de synchronisation :**
   - L'utilisateur choisit un dossier local et un dossier distant.
   - Les préférences sont enregistrées pour chaque serveur.

3. **Surveillance et synchronisation :**
   - Dès qu’un fichier local est ajouté, modifié ou supprimé, les changements sont appliqués au serveur.
   - Si un fichier est supprimé localement, il est également supprimé sur le serveur.

4. **Affichage des logs :**
   - Les actions effectuées sont affichées en temps réel pour permettre un suivi précis.

---

#### **6. Avantages de l’application**

- **Gain de temps** : Plus besoin de gérer manuellement les transferts FTP.
- **Réduction des erreurs** : Synchronisation automatique et suivi des opérations.
- **Support multi-projets** : Adapté aux développeurs travaillant sur plusieurs sites ou environnements.
- **Simplicité d’utilisation** : Interface claire et facile à prendre en main.

---

#### **7. Perspectives d’amélioration**

- **Support SFTP** : Ajouter la compatibilité avec le protocole sécurisé SFTP.
- **Planification** : Permettre une synchronisation programmée à des heures spécifiques.
- **Notifications** : Intégrer des alertes pour signaler les erreurs ou les succès de synchronisation.
- **Support multi-plateformes** : Étendre l’application pour macOS et Linux.

---

#### **8. Conclusion**
**FTP Sync App** est un outil indispensable pour les développeurs web qui travaillent avec des serveurs distants. Grâce à sa synchronisation automatique et à sa gestion intuitive des paires de dossiers, l'application réduit les tâches manuelles répétitives et améliore la productivité. Elle s'est avérée être une solution robuste et fiable lors de mes propres projets de développement web.

Si vous êtes un développeur ou un technicien IT à la recherche d'une solution simple et efficace pour synchroniser des fichiers, **FTP Sync App** est l’outil qu’il vous faut !
