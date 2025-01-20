<template>
  <div class="autocomplete-container">
    <div class="input-group" :class="{'expended': searchInput}">
      <input type="text" class="form-control" placeholder="Recipient's username" v-model.trim="searchInput">
      <button v-if="searchInput" class="btn btn-outline-secondary" type="button" @click="clearSearch()">X</button>
    </div>

    <div class="list-group" v-if="searchInput">
      <button type="button" class="list-group-item list-group-item-action active">
        Cras justo odio
      </button>
      <button type="button" class="list-group-item list-group-item-action">Dapibus ac facilisis in</button>
      <button type="button" class="list-group-item list-group-item-action">Morbi leo risus</button>
      <button type="button" class="list-group-item list-group-item-action">Porta ac consectetur ac</button>
      <button type="button" class="list-group-item list-group-item-action">Vestibulum at eros</button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref } from 'vue';
  
  const searchInput = ref<string | null>(null);

  function clearSearch() {
    searchInput.value = null;
  }
</script>

<style scoped lang="scss">
  .expended {
    input {
      border-bottom-left-radius: 0;
      /*box-shadow: -.25rem 0 10px rgba(102, 153, 153, 0.25), 0 -.25rem 10px rgba(102, 153, 153, 0.25), -.25rem 0 white;*/
    }

    button {
      border-bottom-right-radius: 0;
    }
  }

  .list-group-item:first-child {
    border-top-left-radius: 0;
    border-top-right-radius: 0;
  }

  .autocomplete-container {
    position: relative;
  }

  .list-group {
    position: absolute;
    width: 100%
  }
</style>
