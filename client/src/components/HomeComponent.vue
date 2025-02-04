<template>
  <div class="content">
    <h3>{{ t('home.title') }}</h3>

    <p>{{ t('home.p1') }}</p>

    <p>{{ t('home.p2') }}</p>

    <p>{{ t('home.p3') }}</p>

    <p>{{ t('home.p4') }}</p>

    <p>{{ t('home.p5') }}</p>

    <p>{{ t('home.p6') }}</p>

    <p>{{ t('home.p7') }}</p>

    <p>{{ t('home.p8') }}</p>

    <p>{{ t('home.p9') }}</p>

    <p>{{ t('home.p10') }}</p>
  </div>
</template>

<script setup lang="ts">
  import { ref, onMounted, inject } from 'vue';
  import { useI18n } from 'vue-i18n'
  
  import { type User, AccessGroup } from '../models/user'
  import { AuthenticationService } from '../services/AuthenticationService';

  const loggedUser = ref<User | null>(null);
  const eventBus = inject('eventBus') as any;
  const { t } = useI18n();

  onMounted(() => {
    const loggedUserString = localStorage.getItem(AuthenticationService.localeStorageKey);
    if (loggedUserString) {
      loggedUser.value = JSON.parse(loggedUserString);
    }


    eventBus.on('loggin', (user: User | null) => {
      loggedUser.value = user;
    });
  })
</script>

<style scoped>

  .content {
      text-align: left;
  }

  p {
      margin-bottom: 1rem;
  }
</style>
