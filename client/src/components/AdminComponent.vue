<template>
  <h3>Admin</h3>

  <div v-if="isLoading" class="spinner-border" role="status">
    <span class="sr-only"></span>
  </div>

  <div v-if="!isLoading && data">
    <div class="header-count">
      <div class="comming">
        {{ $t('admin.commingHeader') }} {{ data.comming.length }}
      </div>
      <div class="notComming">
        {{ $t('admin.notCommingHeader') }} {{ data.notComming.length }}
      </div>
      <div class="unAnswered">
        {{ $t('admin.unAnsweredHeader') }} {{ data.unAnswered.length }}
      </div>
    </div>
    <div class="body">
      <ul class="nav nav-tabs">
        <li class="nav-item">
          <a class="nav-link"
             :class="{'active': activeTab == ActiveTab.Comming}"
             href="#"
             @click="navigate(ActiveTab.Comming)">
            {{ $t('admin.comming') }}
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link"
             :class="{'active': activeTab == ActiveTab.NotComming}"
             href="#"
             @click="navigate(ActiveTab.NotComming)">
            {{ $t('admin.notComming') }}
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link"
             :class="{'active': activeTab == ActiveTab.UnAnswered}"
             href="#"
             @click="navigate(ActiveTab.UnAnswered)">
            {{ $t('admin.unAnswered') }}
          </a>
        </li>
      </ul>

      <div v-if="activeTab == ActiveTab.Comming" class="form-container">
        <div v-for="(user, index) in data.comming">
          <FormComponent v-model:user="data.comming[index]" v-model:form="data.comming[index].form" class="form" />
        </div>
      </div>

      <div v-else-if="activeTab == ActiveTab.NotComming" class="form-container">
        <div v-for="(user, index) in data.notComming">
          <FormComponent v-model:user="data.notComming[index]" v-model:form="data.notComming[index].form" class="form" />
        </div>
      </div>

      <div v-else-if="activeTab == ActiveTab.UnAnswered">
        <div v-for="user in data.unAnswered">
          {{ user.firstName }} {{ user.lastName }}
        </div>
      </div>
    </div>
  </div>

</template>

<script setup lang="ts">
  enum ActiveTab {
    Comming,
    NotComming,
    UnAnswered
  }

  import { ref, onMounted } from 'vue';

  import FormComponent from "./FormComponent.vue"

  import { AdminService } from '../services/AdminService'
  import type { Admin } from '@/models/Admin';

  const adminService = new AdminService();

  const isLoading = ref<boolean>(true);
  const data = ref<Admin | null>();
  const activeTab = ref<ActiveTab>(ActiveTab.Comming);

  onMounted(async () => {
    data.value = await adminService.getDatas();
    isLoading.value = false;
  });

  function navigate(targetTab: ActiveTab) {
    activeTab.value = targetTab;
  }
</script>

<style scoped>
  .body {
    margin-top: 2rem;
  }

  .form-container {
    display: flex;
    flex-direction: column;
    row-gap: 1rem;
  }
</style>
