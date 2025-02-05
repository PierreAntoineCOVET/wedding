<template>
  <div class="autocomplete-container">
    <div class="input-group" :class="{'expended': searchInput}">
      <input type="text" class="form-control" placeholder="Recipient's username" v-model.trim="searchInput" />
      <button v-if="searchInput && !isLoading" class="btn btn-outline-secondary" type="button" @click="clearSearch()">X</button>
      <div  v-if="isLoading" class="input-group-text">
        <div class="spinner-border text-primary spinner-border-sm" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>

    <div class="list-group" v-if="searchResults.length">
      <button
              type="button"
              class="list-group-item list-group-item-action"
              v-for="searchResult in searchResults"
              @click="select(searchResult)">
        {{ searchResult.label }}
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ref, watch } from 'vue';
  import debounce from 'lodash.debounce'

  import { UserService } from '../services/UserService'
  import { type AutocompleteItem } from '../models/autocompleteItem'
  
  const searchInput = ref<string | null>(null);
  const isLoading = ref<boolean>(false);
  const searchResults = ref<AutocompleteItem[]>([]);
  const selectedItem = ref<AutocompleteItem | null>(null);

  const userService = new UserService();

  function clearSearch() {
    searchInput.value = null;

    searchResults.value = [];

    selectedItem.value = null;
  }

  function select(result: AutocompleteItem) {
    if (result.value == "-1") {
      return;
    }

    selectedItem.value = result;

    searchInput.value = result.label;

    searchResults.value = [];
  }

  watch(searchInput, debounce(async (newSeach) => { searchUser(newSeach); }, 500));

  async function searchUser(query: string) {
    if (!query || selectedItem.value) {
      return;
    }

    isLoading.value = true;

    const results = await userService.search(query);

    if (results.length == 0) {
      results.push({ value: "-1", label: 'no match found' });
    }

    searchResults.value = results;

    isLoading.value = false;
  }
</script>

<style scoped lang="scss">
  .expended {
    input {
      border-bottom-left-radius: 0;
      box-shadow: none;
    }

    button {
      border-bottom-right-radius: 0;
      border-top-left-radius: 0;
      border-bottom-left-radius: 0;
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
    width: 100%;
    /*margin-top: 0.25rem;*/
  }

  .input-group.expended {
    box-shadow: 0 0 0 0.25rem rgba(102, 153, 153, 0.25);
    border-radius: var(--bs-border-radius);
  }

  .spinner-border {
    border-radius: 50%;
  }
</style>
