<template>
  <h5>Mercredi 6 août 2025</h5>

  <div v-if="isMulti">
    <p>
      Debout ! (Pierre Antoine tu as vu ta tête quand tu as inséré le contenu sur le site ? Ah
      non, bon aller c’était pour rire... c’est l’heure de naissance de Léon)
    </p>

    <p>
      Le matin ! Vous vous gérez comme vous voulez ! Jusqu’à 10h45 (ou plutôt 10h30 pour ceux qui
      auraient du mal à se lever, et oui on a des exemples précis en tête)
    </p>
  </div>
  <div v-else>
    <p>
      Vous êtes conviez à nous rejoindre sur le site de la réception.
    </p>

    <p>
      On vous conseille d’arriver aux alentours de 10h45
    </p>
  </div>

  <p>
    11h : A vos mouchoirs, on a prévu une petite cérémonie laïque. Vous pourrez pleurer d’émotion
    (certes c’est surfait), de rire (plus de chance) ou d’ennuie si jamais on s’est lancé dans des discours
    qui ne s’arrêtent plus (fort peu probable) !
  </p>

  <p>
    12h00 : On passe aux choses sérieuses ! Et on démarre avec le traditionnel vin d’honneur (ne vous
    inquiétez pas, on n’a pas oublié les glaçons).
  </p>

  <p>Dans la foulée, on vous servira à manger (On pense à votre énergie).</p>

  <p>
    Dans l’après-midi : Temps photos (la mariée aime bien les photos et certains seront déjà marqués par
    les épreuves du vin d’honneur, autres boissons et auront des têtes somme toute très mémorables)
  </p>

  <p>Temps libre ! Jeux, jeux en bois, parties de pétanque, balades en montagne, SPA, sauna ...</p>

  <p>
    18h30 : Parce que c’est notre truc de jeunes qui commencent à devenir vieux et qu’on aime bien les
    traditions françaises,(oui ça va les plus anciens on a compris vous ne vous sentez pas concernés)
    APERO !
  </p>

  <p>20h00 : Des petites choses à manger (au cas où vous ayez encore un peu faim)</p>

  <p>21h30 : La traditionnelle pièce montée !</p>

  <p>Fin de soirée en musique ou en jeux, c’est comme vous voulez !</p>
</template>

<script setup lang="ts">
  defineProps<{
    isMulti: boolean
  }>()
</script>

<style scoped>
</style>
