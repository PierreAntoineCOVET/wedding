<template>
  <NavigationComponent />

  <main class="scroll-container">
    <RouterView />
  </main>
</template>

<script setup lang="ts">
  import NavigationComponent from './components/NavigationComponent.vue'
</script>

<style lang="scss">
  $primary: #669999;

  @import "../node_modules/bootstrap/scss/bootstrap";

  .btn,
  .btn:hover {
    color: #fff;
  }
</style>
