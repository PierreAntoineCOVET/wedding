<template>
  <h3 class="title">La foire aux questions</h3>

  <p>
    <h5>Ce que je dois amener :</h5>
    <ul><li>Une serviette de bain (uniquement pour les français)</li></ul>
  </p>

  <p>
    <h5>Ce que je peux prendre avec moi (optionnel) :</h5>
    <ul>
      <li>Un maillot de bain</li>
      <li>Serviette de plage pour les spas et sauna</li>
      <li>Des boules de pétanques (d’autres pourront les utiliser si ce n’est pas vous)</li>
      <li>Une tenue détente et accessoires de toilettes</li>
    </ul>
  </p>

  <div>
    <h5>Est-ce qu’il y a un code couleur ?</h5>

    <p>
      Oui, notre ligne de couleurs tourne autour de l’eucalyptus et du
      rose poudré et tout ce qui est dans les mêmes tons.
    </p>

    <p>
      <div class="color-container">
        <div class="color-sample bg-primary">blush</div>
        <div class="color-sample bg-secondary">eucalyptus</div>
      </div>
    </p>

    <p>
      Bien que rien ne vous oblige à le suivre, nous sommes décidés à
      ne pas tomber dans le traditionnel costume noir ou les tenues
      guindées ! On vous suggère des tenues légères (lin, coton ou
      autres !)
    </p>

    <p>
      Et pour les couleurs, celles juste ici !
    </p>

    <p>
      Chemises, pantalon, chaussettes, chaussures de couleurs à motifs
      (du type tout plein de fleurs) sont plus que bienvenus !
    </p>

    <p>
      On sort des sentiers battus et des traditions ici sauf pour le blanc
      qui reste pour la mariée (enfin on verra bien si elle est en blanc ou
      pas le jour J).
    </p>

    <p>
      Et la cerise sur le gâteau ? Vous pouvez prévoir le short ou le bermudas on est pas contre (mais svp
      pas le short hawaïen !) et troquer les chaussures qui font mal aux pieds pour sandales ou sneakers !
    </p>
  </div>

  <div>
    <h5>Accessoires :</h5>
    <p>
      Casquette, bretelles, nœud papillon, cravate à fleur, ombrelles, sneakers … ! Tout ça
      on valide et encourage !
    </p>
  </div>

  <div>
    <h5>Est-ce qu’il y a un thème ?</h5>
    <p>
      Bonne question, mais elle n’a pas vraiment de réponse ! On se situe entre le traditionnel vu et revu
      « champêtre », et le « vintage ». Ce que l’on souhaite vraiment c’est que tout soit dans le « confort »,
      « la simplicité » la « bonne humeur » et sans jugement, le thème n’a du coup pas grande
      importance !
    </p>
  </div>

  <div>
    <h5>Est-ce qu’on a déposé une traditionnelle liste de mariage ?</h5>
    <p>
      Non, évidemment notre plus beau cadeau c’est vous, mais du fait que certains d’entre vous sont
      têtus une boîte absolument pas secrète sera disponible sur place. Vous pouvez évidemment y mettre
      un euro en chocolat ! (mais noir de préférence).
    </p>
  </div>

  <div>
    <h5>Est-ce qu’on a une nounou ?</h5>
    <p>
      Bien que ça reste à confirmer, une jeune fille habituée à faire du babysitting devrait être présente
      pour s’occuper des enfants. Attention on lui demande surtout de surveiller qu’ils soient bien présents
      sur le site et de s’amuser avec eux mais vos enfants restent sous votre responsabilité (surtout pour
      les moins de 3 ans).
    </p>
  </div>

  <div>
    <h5>Est-ce que je peux dormir sur place ?</h5>
    <p v-if="hasMultipleDay()">
      Vous avez reçu une invitation sur plusieurs jours, le logement sera disponible sur place. Le lieu
      n’acceptant pas plus de 60 personnes sur place au même moment, vous verrez des invités différents
      en fonction des jours mais vous vous serez là tout du long !
    </p>
    <p>
      Il existe des logements sur place, mais ils seront occupés par les personnes qui viennent de loin, les
      mariés et leurs témoins.
    </p>
    <p>
      Il est possible de loger dans les villes proches si vous ne souhaitez pas repartir en plein milieu de la
      nuit :
      <ul>
        <li><a href="https://www.facebook.com/labuttardiere/" target="_blank">La Buttardière - Gîte et B&amp;B</a></li>
        <li><a href="https://www.gitedugrandcucheron.fr/" target="_blank">Gîte du Grand Cucheron</a></li>
        <li><a href="https://maurienne-outdoor.com/eco-camping/" target="_blank">Hebergement insolites</a></li>
        <li><a href="https://www.logishotels.com/fr/hotel/logis-hotel-la-cle-des-champs-14456?partid=1535" target="_blank">Hôtel Logis La Clef des Champs (Montmélian 30km)</a></li>
        <li>et plein d’autres ! N’hésitez pas à nous contacter si vous avez besoin de conseils.</li>
      </ul>
    </p>
  </div>

  <div>
    <h5>Les logements :</h5>
    <p>
      A l’entrée de chaque gîte se trouvera une carte avec l’emplacement des chambres et vos noms inscrits
      dessus. Sur chaque porte sera indiqué votre nom.
    </p>
    <p>
      Il y a 3 gîtes :
      <ul>
        <li>Le Chalet à Mahé</li>
        <li>La Grange à Lou</li>
        <li>La Grange à Line</li>
      </ul>
    </p>
    <p>Les deux derniers sont mitoyens mais les accès sont bien distincts.</p>
    <p>
      Sur place les draps sont fournis pour chaque lit. Par contre, la serviette de bain n’est pas fournie tout
      comme celle de plage (qui est optionnelle).
    </p>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';

  import { type User } from '../models/User'
  import { UserService } from '../services/UserService'

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;

  onMounted(() => {
    const loggedUserString = localStorage.getItem(UserService.localeStorageKey);
    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }


    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;
    });
  })

  function hasMultipleDay(): boolean {
    if (!loggedUser.value) {
      return false;
    }

    const charArray = loggedUser.value.invitation.split('');
    const numberOfOne = charArray.reduce(
      (count, char) => {
        console.log(count);
        console.log(char);
        return char === '1' ? ++count : count;
      },
      0)

    return numberOfOne > 1;
  }
</script>

<style scoped>
  .title {
    margin-bottom: 1rem;
  }

  .color-container {
      display: flex;
  }

  .color-sample {
    text-align: center;
    width: 100px;
    height: 100px;
    margin-right: 20px;
    border-radius: 50%;
    line-height: 95px;
  }
</style>
