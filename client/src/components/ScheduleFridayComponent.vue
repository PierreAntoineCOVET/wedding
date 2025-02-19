<template>
  <h5>Vendredi 8 août 2025</h5>

  <div v-if="isMulti">
    <p>
      On libère la grande salle à 8h00 (en résumé, vos petits effets personnels devront
      déménager dans les gites la veille !)
    </p>
  </div>

  <p>
    Vous pourrez profiter et vous détendre (et/ou) nous aider à préparer le déménagement retour de
    toutes nos petites affaires
  </p>

  <div class="schedule-grid">
    <div class="time">11h00</div>
    <div class="info">
      <p>
        Clap de fin, snif ! Il faut partir !
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
  defineProps<{
    isMulti: boolean
  }>()
</script>

<style scoped>
  @media (min-width: 1024px) {
    .schedule-grid {
      display: grid;
      grid-template-columns: 55px auto;
    }
  }
</style>
