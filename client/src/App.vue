<template>
  <v-app>
    <div id="nav-container">
      <NavigationComponent id="always-on-top" />
    </div>

    <main class="scroll-container">
      <RouterView />
    </main>
  </v-app>
</template>

<script setup lang="ts">
  import NavigationComponent from './components/NavigationComponent.vue'
</script>

<style lang="scss">
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
