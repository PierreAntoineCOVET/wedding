<template>
  <h5>Jeudi 7 août 2025</h5>

  <div v-if="isMulti">
    <p>
      Peu importe le moment que vous avez passez avec nous sur notre semaine marathon mariage,
      jeudi c’est journée récup et partage et vous êtes tous, bienvenus !
    </p>
  </div>

  <p>
    11h30 : Brunch sucré-Salé
  </p>

  <p>
    Et on profite de la journée ! (qu’on espère ensoleillée) ! (Et les mariés rangeront un peu en fin de
    journée).
  </p>
</template>

<script setup lang="ts">
  defineProps<{
    isMulti: boolean
  }>()
</script>

<style scoped>
</style>
