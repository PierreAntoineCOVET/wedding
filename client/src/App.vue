<template>
  <NavigationComponent />

  <main class="scroll-container">
    <RouterView />
  </main>
</template>

<script setup lang="ts">
  import NavigationComponent from './components/NavigationComponent.vue'
  import 'bootstrap/dist/js/bootstrap.min.js'
  //import "@popperjs/core/dist/umd/popper.min.js"
</script>

<style lang="scss">
  $primary: #669999;

  @import "bootstrap/scss/bootstrap";
</style>
