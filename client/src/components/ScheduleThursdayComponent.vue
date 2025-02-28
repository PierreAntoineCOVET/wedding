<template>
  <h5>Jeudi 7 août 2025</h5>

  <div v-if="isMulti">
    <p>
      Peu importe le moment que vous avez passez avec nous sur notre semaine marathon mariage,
      jeudi c’est journée récup et partage et vous êtes tous, bienvenus !
    </p>
  </div>

  <div class="schedule-grid">
    <div class="time">11h30</div>
    <div class="info">
      <p>
        Brunch sucré-Salé
      </p>
    </div>
  </div>

  <p>
    Et on profite de la journée, qu’on espère ensoleillée ! (Et les mariés rangeront un peu en fin de
    journée).
  </p>
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
