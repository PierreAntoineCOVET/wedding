<template>
  <div id="nav-container">
    <NavigationComponent id="always-on-top" />
  </div>

  <main class="scroll-container">
    <RouterView />
  </main>
</template>

<script setup lang="ts">
  import 'bootstrap/dist/js/bootstrap.min.js'

  import NavigationComponent from './components/NavigationComponent.vue'
</script>

<style lang="scss">
  $primary: #669999;

  @import "bootstrap/scss/bootstrap";

  #always-on-top {
    z-index: 100;
  }

  #nav-container {
      padding-top: 1rem;
      padding-bottom: .1rem;
      top: 0;
      position: sticky;
      background-color: white;
  }
</style>
